using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DirectShowLib;

namespace media_player_demo
{
    enum tState
    {
        EMPTY,
        LOADED,
        PAUSED,
        STOPPED,
        PLAYING
    };
    struct ttime
    {
        public int hour;
        public int minute;
        public int second;
    };
    public partial class Form1 : Form
    {
        /// Volume declarations.
        private const int minimumVolume = -10000;
        private const int maximumVolume = 0;
        private bool mute = false;
        private int volume = maximumVolume;
        /// Graph declarations.
        private IGraphBuilder graphBuilder = null;
        private IMediaControl mediaControl = null;
        private IBasicAudio basicAudio = null;
        private IMediaSeeking mediaSeeking = null;
        private IMediaEvent mediaEvent = null;
        private IVideoWindow videoWindow = null;
        /// Source type and time declarations.
        private string fileName = string.Empty;
        private string fileExtension = string.Empty;
        private tState currentState = tState.EMPTY;
        private long totalDuration = 0;
        private long currentDuration = 0;
        private bool isVideo = false;
        private bool stopEvent = false;
        private ttime playTime;
        private ttime pauseStopTime;
        private string storePlayTime = string.Empty;
        private Rectangle fullScreenSize = Screen.PrimaryScreen.WorkingArea;
        private Rectangle panelOriginalSize;

        public Form1()
        {
            InitializeComponent();
            /// Create graph filter manager to handle the new instance.
            CreateAndInitializeGraph();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /// Store videoPanel's original dimensions.
            panelOriginalSize.Size = videoPanel.Size;
            panelOriginalSize.Location = videoPanel.Location;
        }
        private void openkey_Click(object sender, EventArgs e)
        {
            string updateStatus = string.Empty;
            /// File formats to filter from the open-select window.
            openFileDialog.Filter = "Common Media Files (.MP3, .MP4, .WAV, .OGG, .WMA, .AVI, .MPEG, .WMV, .MOV, .DIVX)| " +
                                    "*.MP3;*.WAV;*.OGG;*.WMA;*.AVI;*.MP4;*.MPEG;*.WMV;*.MOV;*.DIVX;.*3GP|All Files (*.*)|*.*";
            openFileDialog.FileName = string.Empty;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                timer.Enabled = false;
                fileName = openFileDialog.FileName;
                filePath.Text = fileName;
                pictureBox1.Visible = true;
                /// Get file extension in UPPER case.
                fileExtension = Path.GetExtension(fileName).ToUpper();      
                if (fileExtension == ".WMV" || fileExtension == ".MOV" || fileExtension == ".AVI" || fileExtension == ".DIVX" ||
                    fileExtension == ".MPEG" || fileExtension == ".MPG" || fileExtension == ".MP4" || fileExtension == ".3GP" ||
                    fileExtension == ".3GPP" || fileExtension == ".AMV" || fileExtension == ".ASF")
                {
                    isVideo = true;
                }
                else
                {
                    isVideo = false;
                }
                /// Clear graph and all its resources.
                Cleanup();
                System.Threading.Thread.Sleep(50);
                /// Create graph filter manager to handle the new instance. 
                CreateAndInitializeGraph();
                System.Threading.Thread.Sleep(50);
                /// Start rendering.
                this.graphBuilder.RenderFile(fileName, null);
                /// Get total play time of loaded file.
                this.mediaSeeking.GetDuration(out totalDuration);
                TimeFormat(totalDuration, out playTime);
                updateStatus = "File loaded.\t  Total duration = ";
                updateStatus += playTime.hour.ToString("D" + 2) + ":";
                updateStatus += playTime.minute.ToString("D" + 2) + ":";
                updateStatus += playTime.second.ToString("D" + 2);
                statusBar.Text = updateStatus;
                storePlayTime = statusBar.Text.Substring(32, 8);
                currentState = tState.LOADED;
            }
        }
        /// Run all filters in the graph.
        private void playKey_Click(object sender, EventArgs e)
        {
            string updateStatus = string.Empty;
            if (fileName == string.Empty || currentState == tState.EMPTY)
            {
                return;
            }
            if (currentState != tState.PLAYING && currentState != tState.PAUSED)
            {
                /// Make videoPanel as owner of play window.
                this.videoWindow.put_Owner(videoPanel.Handle);
                /// Set video window style.
                this.videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings);
                /// Set video window dimensions.
                this.videoWindow.SetWindowPosition(0, 0, videoPanel.Width, videoPanel.Height);
                /// Set video window visible.
                this.videoWindow.put_Visible(OABool.True);
                System.Threading.Thread.Sleep(50);
                /// video panel will handle the video window messages.
                this.videoWindow.put_MessageDrain(videoPanel.Handle);
            }
            if (isVideo == true)
            {
                pictureBox1.Visible = false;
            }
            else
            {
                pictureBox1.Visible = true;
            }
            if (currentState != tState.PLAYING)
            {
                timer.Enabled = true;
                System.Threading.Thread.Sleep(50);
                /// Run all the filters in the filter graph.
                this.mediaControl.Run();
            }
            /// Set volume.
            if (mute == true)
            {
                updateStatus = "Playing...(Muted)      Total duration = ";
                this.basicAudio.put_Volume(minimumVolume);
            }
            else
            {
                updateStatus = "Playing...\t\t  Total duration = ";
                this.basicAudio.put_Volume(volume);
            }
            currentState = tState.PLAYING;
            updateStatus += storePlayTime;
            statusBar.Text = updateStatus;
        }
        /// Pause all filters.
        private void pauseKey_Click(object sender, EventArgs e)
        {
            string updateStatus = string.Empty;
            timer.Enabled = false;
            if (currentState == tState.PLAYING)
            {
                /// Pauses all the filters in the filter graph.
                this.mediaControl.Pause();
                this.mediaSeeking.GetCurrentPosition(out currentDuration);
                TimeFormat(currentDuration, out pauseStopTime);
                updateStatus = "Paused.\t             Paused duration = ";
                updateStatus += pauseStopTime.hour.ToString("D" + 2) + ":";
                updateStatus += pauseStopTime.minute.ToString("D" + 2) + ":";
                updateStatus += pauseStopTime.second.ToString("D" + 2);
                statusBar.Text = updateStatus;
                currentState = tState.PAUSED;
            }
            else
            {
                statusBar.Text = "First play the file.";
            }
        }
        /// Stop all filters and reset graph builder.
        private void stopKey_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if (currentState == tState.PLAYING || currentState == tState.PAUSED)
            {
                /// Stops all the filters in the filter graph.
                this.mediaControl.Stop();
                if (stopEvent == false)
                {
                    statusBar.Text = "Stopped.";
                }
                else
                {
                    statusBar.Text = "Completed (Press Play to restart).";
                    stopEvent = false;
                }
                pictureBox1.Visible = true;
                pictureBox1.BringToFront();
                currentState = tState.STOPPED;
                /// Clear graph and all its resources.
                Cleanup();
                System.Threading.Thread.Sleep(50);
                /// Create graph filter manager to handle the new instance. 
                CreateAndInitializeGraph();
                System.Threading.Thread.Sleep(50);
                /// Start rendering on output pin of source filter.
                this.graphBuilder.RenderFile(fileName, null);
            }
            else
            {
                statusBar.Text = "First play the file.";
            }
        }
        /// Mute volume.
        private void muteKey_Click(object sender, EventArgs e)
        {
            if (currentState != tState.EMPTY && currentState != tState.LOADED)
            {
                this.basicAudio.put_Volume(minimumVolume);
            }
            mute = true;
            statusBar.Text = "Volume muted.";
        }
        /// Decrease volume.
        private void lessVolumeKey_Click(object sender, EventArgs e)
        {
            mute = false;
            volume -= 250;
            if (volume <= minimumVolume)
            {
                volume = minimumVolume;
                statusBar.Text = "Minimum Volume.";
            }
            else
            {
                statusBar.Text = "Volume decreased.";
            }
            if (currentState != tState.EMPTY && currentState != tState.LOADED)
            {
                this.basicAudio.put_Volume(volume);
            }
        }
        /// Increase volume.
        private void moreVolumeKey_Click(object sender, EventArgs e)
        {
            mute = false;
            volume += 250;
            if (volume >= maximumVolume)
            {
                volume = maximumVolume;
                statusBar.Text = "Maximum Volume.";
            }
            else
            {
                statusBar.Text = "Volume increased.";
            }
            if (currentState != tState.EMPTY && currentState != tState.LOADED)
            {
                this.basicAudio.put_Volume(volume);
            }
        }
        /// Restore all defaults.
        private void removeKey_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if (fileName != string.Empty)
            {
                /// Stops all the filters in the filter graph.
                this.mediaControl.Stop();
            }
            pictureBox1.Visible = true;
            pictureBox1.BringToFront();
            fileName = string.Empty;
            currentState = tState.EMPTY;
            filePath.Text = "(No file selected)";
            statusBar.Text = "All default settings restored.";
            volume = maximumVolume;
        }
        /// Create graph filter manager to handle the new instance.
        private void CreateAndInitializeGraph()
        {
            this.graphBuilder = (IGraphBuilder)new FilterGraph();
            this.mediaControl = (IMediaControl)this.graphBuilder;
            this.basicAudio = this.graphBuilder as IBasicAudio;
            this.mediaSeeking = this.graphBuilder as IMediaSeeking;
            this.mediaEvent = this.graphBuilder as IMediaEvent;
            this.videoWindow = this.graphBuilder as IVideoWindow;
        }
        /// Convert time duration to hour, minute, second format.
        void TimeFormat(long timeDuration, out ttime timeObject)
        {
            double duration;
            /// Each unit of timeDuration is 10000000ns (MSDN).
            duration = ((double)timeDuration) / 10000000;
            timeObject.hour = (byte)(duration / 3600);
            duration = duration % 3600;
            duration = duration / 60;
            timeObject.minute = (byte)(duration);
            duration -= (double)timeObject.minute;
            timeObject.second = (byte)(duration * 60);
        }
        /// Toggle between normal screen size and full screen size.
        private void ToggleFullScreen(object sender, EventArgs e)
        {
            OABool mode;
            if (isVideo == true && (currentState == tState.PLAYING || currentState == tState.PAUSED))
            {
                this.videoWindow.get_FullScreenMode(out mode);
                if (mode == OABool.False)
                {
                    /// Enter full screen mode.
                    this.videoWindow.put_FullScreenMode(OABool.True);
                    videoPanel.Location = fullScreenSize.Location;
                    videoPanel.Size = fullScreenSize.Size;
                }
                else
                {
                    videoPanel.Location = panelOriginalSize.Location;
                    videoPanel.Size = panelOriginalSize.Size;
                    /// Leave full screen mode.
                    this.videoWindow.put_FullScreenMode(OABool.False);
                    this.videoWindow.SetWindowPosition(0, 0, videoPanel.Width, videoPanel.Height);
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            string updateStatus = string.Empty;
            this.mediaSeeking.GetCurrentPosition(out currentDuration);
            if (currentDuration >= totalDuration)
            {
                timer.Enabled = false;
                stopEvent = true;
                stopKey_Click(sender, e);
                return;
            }
            TimeFormat(currentDuration, out playTime);
            updateStatus = "Current = ";
            updateStatus += playTime.hour.ToString("D" + 2) + ":";
            updateStatus += playTime.minute.ToString("D" + 2) + ":";
            updateStatus += playTime.second.ToString("D" + 2);
            updateStatus += "\t\tTotal = " + storePlayTime;
            statusBar.Text = updateStatus;
        }
        /// Clear graph and all its resources.
        private void Cleanup()
        {
            Marshal.ReleaseComObject(mediaEvent);
            Marshal.ReleaseComObject(mediaSeeking);
            Marshal.ReleaseComObject(basicAudio);
            Marshal.ReleaseComObject(mediaControl);
            Marshal.ReleaseComObject(videoWindow);
            Marshal.ReleaseComObject(graphBuilder);
        }
        private void Form1_closing(object sender, EventArgs e)
        {
            /// Clear graph and all its resources.
            Cleanup();
        }
        private void closeKey_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}