using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BuildWatch.Properties;
using System.Media;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace BuildWatch
{
    public partial class MainForm : Form
    {
        public const string AppVersion = "0.9";

        enum BuildColor
        {
            GREEN, RED, WHITE,
            STALE, FENIX,
            JUSTGREEN, JUSTRED,
            OFF
        }

        class BuildInfo
        {
            public string Name { get; set; }
            public BuildColor Color { get; set; }
            public DateTime FinishTime { get; set; }
            public string User { get; set; }

            public bool UserIsOurs { get; set; }
            public BuildColor DisplayColor { get; set; }
            public DateTime ColorPoint { get; set; }

            public BuildInfo(BuildWatchWorker.BuildInfo bi, BuildInfo prev, DateTime stalePoint, DateTime forgetPoint)
            {
                Name = bi.Name;
                Color = Convert(bi.Color);
                FinishTime = bi.FinishTime;

                string user = bi.User;
                int p = user.LastIndexOf('\\');
                if (p >= 0)
                    user = user.Substring(p + 1);
                if (!string.IsNullOrEmpty(Settings.Default.OurUserMatch))
                {
                    UserIsOurs = Regex.IsMatch(user, Settings.Default.OurUserMatch);
                }
                else
                {
                    UserIsOurs = true;
                }
                if (!string.IsNullOrEmpty(Settings.Default.UserNameMatch))
                {
                    var match = Regex.Match(user, Settings.Default.UserNameMatch);
                    if (match.Success)
                        user = string.Format(Settings.Default.UserNameReplace, match.Groups.Cast<object>().ToArray());
                }
                User = user;

                if (FinishTime != default(DateTime)
                    && FinishTime < forgetPoint)
                {
                    DisplayColor = BuildColor.OFF;
                }
                else if (Color == BuildColor.RED
                    && FinishTime != default(DateTime)
                    && FinishTime < stalePoint)
                {
                    DisplayColor = BuildColor.STALE;
                }
                else if (Color == BuildColor.RED
                    && prev != null
                    && (prev.DisplayColor == BuildColor.OFF || prev.DisplayColor == BuildColor.FENIX))
                {
                    if (prev.DisplayColor == BuildColor.OFF)
                    {
                        DisplayColor = BuildColor.FENIX;
                        ColorPoint = DateTime.Now.AddHours(6);
                    }
                    else if (prev.ColorPoint > DateTime.Now)
                    {
                        DisplayColor = BuildColor.FENIX;
                        ColorPoint = prev.ColorPoint;
                    }
                    else
                    {
                        DisplayColor = BuildColor.RED;
                    }
                }
                else if (Color == BuildColor.RED
                    && prev != null
                    && prev.Color == BuildColor.GREEN)
                {
                    DisplayColor = BuildColor.JUSTRED;
                }
                else if (Color == BuildColor.GREEN
                    && prev != null
                    && prev.Color == BuildColor.RED)
                {
                    DisplayColor = BuildColor.JUSTGREEN;
                }
                else
                {
                    DisplayColor = Color;
                }
            }

            private BuildColor Convert(BuildWatchWorker.BuildColor buildColor)
            {
                switch (buildColor)
                {
                    case BuildWatchWorker.BuildColor.GREEN:
                        return BuildColor.GREEN;
                    case BuildWatchWorker.BuildColor.RED:
                        return BuildColor.RED;
                    default:
                        return BuildColor.WHITE;
                }
            }
        }

        private BuildWatchWorker.IWorkerThread worker;
        private SoundPlayer redBuildUsPlayer;
        private SoundPlayer redBuildThemPlayer;
        private SoundPlayer greenBuildUsPlayer;
        private SoundPlayer greenBuildThemPlayer;
        private SoundPlayer greenBuildAllPlayer;
        private SoundPlayer soundCheckPlayer;
        private DateTime lastAllGreen;
        private Dictionary<string, BuildInfo> oldInfo;
        private bool resizing;
        private bool inTimer;
        private string buildMatrix;
        private int soundCheckCount;
        private DateTime soundCheckTime;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string versionString;
#if !DEBUG
            versionString = "v" + AppVersion;
#else
            versionString =  " (DEBUG BUILD)";
#endif
            Text = string.Format("Build Watcher {0} - The Ultimate TFS Build Notificator", versionString);
            WindowState = FormWindowState.Maximized;
            DoubleBuffered(topList, true);

            LogPersist("Starting");

            Log("Loading sounds...");
            redBuildUsPlayer = new SoundPlayer(Settings.Default.RedBuildUsSound);
            redBuildThemPlayer = new SoundPlayer(Settings.Default.RedBuildThemSound);
            greenBuildUsPlayer = new SoundPlayer(Settings.Default.GreenBuildUsSound);
            greenBuildThemPlayer = new SoundPlayer(Settings.Default.GreenBuildThemSound);
            greenBuildAllPlayer = new SoundPlayer(Settings.Default.GreenBuildAllSound);
            soundCheckPlayer = new SoundPlayer(Settings.Default.SoundCheckSound);

            if (!string.IsNullOrEmpty(Settings.Default.TfsServerUri))
            {
                Log("Initializing connection to TFS: " + Settings.Default.TfsServerUri);
                worker = new BuildWatchWorker.WorkerThread();
                worker.InitConnection(this);
            }
            else
            {
                Log("TFS Server URI not defined, starting in DEMO MODE");
                worker = new BuildWatchWorker.DemoWorkerThread();
            }
            
            Log("Loading color state...");
            lastAllGreen = DateTime.Now;
            do
            {
                try
                {
                    using (FileStream fs = File.Open(Settings.Default.ColorStateFile, FileMode.Open))
                    {
                        var bin = new BinaryReader(fs);
                        var now = lastAllGreen;
                        var when = new DateTime(bin.ReadInt64());
                        var green = new DateTime(bin.ReadInt64());
                        if (when < now.AddDays(-3) || when > now)
                        {
                            Log("Discarding invalid saved values");
                            break;
                        }
                        lastAllGreen = green;
                        //lastAllGreen = DateTime.Now.AddDays(-4);
                        Log("lastAllGreen restored to " + lastAllGreen);
                    }
                }
                catch (Exception ex)
                {
                    Log(string.Format("ERROR: {0}: {1}", ex.GetType().FullName, ex.Message));
                }
            } while (false);

            Log("Starting worker thread...");
            worker.Start();
            pingTimer.Enabled = true;
            Log("Resuming operation.");
        }

        private void Log(string message, bool raw = false)
        {
            if (logBoxTxt.Lines.Length >= 200)
            {
                string[] lines = new string[100];
                Array.Copy(logBoxTxt.Lines, logBoxTxt.Lines.Length - lines.Length, lines, 0, lines.Length);
                logBoxTxt.Lines = lines;
            }
            if (raw)
                logBoxTxt.AppendText(message);
            else
            {
                logBoxTxt.AppendText(string.Format("[{0}] {1}\n", DateTime.Now, message));
                Application.DoEvents();
            }
        }

        private void LogPersist(string message, bool inFile = true)
        {
            if (persistLogBoxTxt.Lines.Length >= 200)
            {
                string[] lines = new string[100];
                Array.Copy(persistLogBoxTxt.Lines, persistLogBoxTxt.Lines.Length - lines.Length, lines, 0, lines.Length);
                persistLogBoxTxt.Lines = lines;
            }
            string text = string.Format("[{0}] {1}\n", DateTime.Now, message);
            persistLogBoxTxt.AppendText(text);
            Application.DoEvents();
            try
            {
                File.AppendAllLines(Settings.Default.LogFile, new string[] { text });
            }
            catch (Exception ex)
            {
                LogPersist(string.Format("Failed to save log: {0}: {1}", ex.GetType().FullName, ex.Message), false);
            }
        }

        private void pingTimer_Tick(object sender, EventArgs e)
        {
            if (inTimer)
                return;

            inTimer = true;
            try
            {
                List<string> logMessages = worker.RetrieveLogMessages();
                if (logMessages != null && logMessages.Count > 0)
                {
                    foreach (string msg in logMessages)
                    {
                        Log(msg, true);
                    }
                }

                List<BuildWatchWorker.BuildInfo> buildTop = worker.RetrieveBuildTop();
                if (buildTop != null)
                {
                    bool redBuildUs = false;
                    bool redBuildThem = false;
                    bool greenBuildUs = false;
                    bool greenBuildThem = false;
                    bool allGreen = true;
                    if (oldInfo == null)
                        oldInfo = new Dictionary<string, BuildInfo>();
                    var newInfo = new Dictionary<string, BuildInfo>();
                    var matrixBuilder = new StringBuilder();
                    var stalePoint = DateTime.Now.AddDays(-5);
                    var forgetPoint = stalePoint.AddDays(-5);
                    topList.BeginUpdate();
                    try
                    {
                        topList.Items.Clear();
                        foreach (BuildWatchWorker.BuildInfo buildInfo in buildTop)
                        {
                            matrixBuilder.Append(string.Format("|{0}={1}", buildInfo.Name, buildInfo.Color));

                            BuildInfo oldBuild;
                            if (!oldInfo.TryGetValue(buildInfo.Name, out oldBuild))
                                oldBuild = null;

                            var bi = new BuildInfo(buildInfo, oldBuild, stalePoint, forgetPoint);

                            if (oldBuild != null)
                            {
                                if (oldBuild.Color == BuildColor.GREEN &&
                                    bi.Color == BuildColor.RED)
                                {
                                    if (bi.UserIsOurs)
                                        redBuildUs = true;
                                    else
                                        redBuildThem = true;
                                }
                                else if (oldBuild.Color == BuildColor.RED &&
                                    bi.Color == BuildColor.GREEN)
                                {
                                    if (bi.UserIsOurs)
                                        greenBuildUs = true;
                                    else
                                        greenBuildThem = true;
                                }
                            }
                            newInfo[bi.Name] = bi;

                            if (bi.DisplayColor == BuildColor.OFF)
                            {
                                continue;
                            }

                            if (bi.Color != BuildColor.GREEN)
                                allGreen = false;

                            var item = new ListViewItem(bi.Name);
                            item.SubItems.Add(bi.Color.ToString());
                            if (bi.FinishTime != default(DateTime))
                                item.SubItems.Add(bi.FinishTime.ToString());
                            else
                                item.SubItems.Add("");
                            item.SubItems.Add(bi.User);

                            switch (bi.DisplayColor)
                            {
                                case BuildColor.GREEN:
                                    item.BackColor = Color.Green;
                                    item.ForeColor = Color.Silver;
                                    break;
                                case BuildColor.RED:
                                    item.BackColor = Color.Red;
                                    item.ForeColor = Color.Yellow;
                                    break;
                                case BuildColor.STALE:
                                    item.BackColor = Color.Blue;
                                    item.ForeColor = Color.Yellow;
                                    break;
                                case BuildColor.FENIX:
                                    item.BackColor = Color.Magenta;
                                    item.ForeColor = Color.Yellow;
                                    break;
                                case BuildColor.JUSTRED:
                                    item.BackColor = Color.Orange;
                                    item.ForeColor = Color.Black;
                                    break;
                                case BuildColor.JUSTGREEN:
                                    item.BackColor = Color.DarkGreen;
                                    item.ForeColor = Color.Yellow;
                                    break;
                                default:
                                    item.BackColor = Color.White;
                                    item.ForeColor = Color.Black;
                                    break;
                            }
                            topList.Items.Add(item);
                        }
                    }
                    finally
                    {
                        topList.EndUpdate();
                    }

                    oldInfo = newInfo;
                    string newMatrix = matrixBuilder.ToString();
                    if (newMatrix != buildMatrix)
                    {
                        LogPersist("Matrix: " + newMatrix);
                        buildMatrix = newMatrix;
                    }

                    if (redBuildUs)
                        Play(redBuildUsPlayer, "RedUs");
                    else if (redBuildThem)
                        Play(redBuildThemPlayer, "RedThem");
                    else if (allGreen && (greenBuildUs || greenBuildThem) && lastAllGreen < DateTime.Now.AddDays(-3))
                        Play(greenBuildAllPlayer, "GreenAll");
                    else if (greenBuildUs)
                        Play(greenBuildUsPlayer, "GreenUs");
                    else if (greenBuildThem)
                        Play(greenBuildThemPlayer, "GreenThem");

                    if (allGreen)
                        lastAllGreen = DateTime.Now;

                    try
                    {
                        Log("Saving lastAllGreen as " + lastAllGreen);
                        using (FileStream fs = File.Open(Settings.Default.ColorStateFile, FileMode.Create))
                        {
                            var bin = new BinaryWriter(fs);
                            bin.Write(DateTime.Now.Ticks);
                            bin.Write(lastAllGreen.Ticks);
                        }
                        Log("Color state saved.");
                    }
                    catch (Exception ex)
                    {
                        Log(string.Format("ERROR: {0}: {1}", ex.GetType().FullName, ex.Message));
                    }
                }
            }
            finally
            {
                inTimer = false;
            }
        }

        private void Play(SoundPlayer player, string name)
        {
            LogPersist("Play " + name);
            player.Play();
        }

        private void soundCheckBtn_Click(object sender, EventArgs e)
        {
            if (soundCheckTime < DateTime.Now.AddSeconds(-5))
                soundCheckCount = 0;
            else
                soundCheckCount++;
            soundCheckTime = DateTime.Now;
            switch (soundCheckCount)
            {
                case 0:
                case 5:
                    Play(soundCheckPlayer, "Check");
                    break;
                case 1:
                case 6:
                    Play(redBuildUsPlayer, "RedUs:Check");
                    break;
                case 2:
                case 7:
                    Play(greenBuildUsPlayer, "GreenUs:Check");
                    break;
                case 3:
                case 8:
                    Play(redBuildThemPlayer, "RedThem:Check");
                    break;
                case 4:
                case 9:
                    Play(greenBuildThemPlayer, "GreenThem:Check");
                    break;
                case 10:
                    Play(greenBuildAllPlayer, "GreenAll:Check");
                    break;
                default:
                    Play(soundCheckPlayer, "Check");
                    soundCheckCount = 0;
                    break;
            }
        }
        
        private static new void DoubleBuffered(Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }

        private void topList_ClientSizeChanged(object sender, EventArgs e)
        {
            if (resizing)
                return;
            resizing = true;
            try
            {
                int columnWidth = 0;
                foreach (ColumnHeader col in topList.Columns)
                {
                    columnWidth += col.Width;
                }
                int controlWidth = topList.ClientSize.Width;
                if (controlWidth <= 0)
                    return;
                foreach (ColumnHeader col in topList.Columns)
                {
                    int newWidth = col.Width * controlWidth / columnWidth;
                    columnWidth -= col.Width;
                    col.Width = newWidth;
                    controlWidth -= newWidth;
                }
            }
            finally
            {
                resizing = false;
            }
        }
    }
}
