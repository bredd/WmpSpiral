using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WmpSpiral
{
    
    public partial class MainWindow : Form
    {
        RemoteWMP.RemotedWindowsMediaPlayer remotePlayer;

        public MainWindow()
        {
            InitializeComponent();

            // Add the player control (though there's no visual interaction, it needs the ActiveX support)
            remotePlayer = new RemoteWMP.RemotedWindowsMediaPlayer();
            remotePlayer.Dock = DockStyle.Bottom;
            remotePlayer.Location = new System.Drawing.Point(0,0);
            remotePlayer.Size = new System.Drawing.Size(5, 5);
            Controls.Add(remotePlayer);
        }

        private List<MediaHolder> RetrievePlayList()
        {
            WMPLib.IWMPPlaylist playlist = remotePlayer.Player.currentPlaylist;

            int playListCount = playlist.count;
            List<MediaHolder> list = new List<MediaHolder>(playListCount);
            for (int i = 0; i < playListCount; ++i)
            {
                list.Add(new MediaHolder(playlist.Item[i]));
            }
            return list;
        }

        private void PostPlayList(List<MediaHolder> list)
        {
            WMPLib.IWMPPlaylist playlist = remotePlayer.Player.currentPlaylist;

            playlist.clear();
            MediaHolder lastItem = null;
            foreach (MediaHolder holder in list)
            {
                // Prevents the same thing from being played twice in a row
                if (lastItem != null && holder.IsSameAs(lastItem)) continue;
                playlist.appendItem(holder.Media);
                lastItem = holder;
            }
        }

        // This is most useful if the list has already been sorted.
        private void RemoveConsecutiveDuplicates(List<MediaHolder> list)
        {
            if (list.Count < 2) return;
            MediaHolder lastItem = list[0]; 
            int i = 1;
            while (i < list.Count)
            {
                if (list[i].IsSameAs(lastItem))
                {
                    list.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            remotePlayer.Player.controls.play();
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            remotePlayer.Player.controls.pause();
        }

        private void button_Spiral_Click(object sender, EventArgs e)
        {
            List<MediaHolder> list = RetrievePlayList();
            list.Sort(delegate(MediaHolder a, MediaHolder b)
            {
                int comp = a.Track - b.Track;
                if (comp != 0) return comp;
                comp = String.Compare(a.Album, b.Album, StringComparison.Ordinal);
                if (comp != 0) return comp;
                return String.Compare(a.Title, b.Title, StringComparison.Ordinal);
            });
            PostPlayList(list);
        }

        private void SpiralShuffle_Click(object sender, EventArgs e)
        {
            List<MediaHolder> list = RetrievePlayList();

            // Remove duplicates
            list.Sort();
            RemoveConsecutiveDuplicates(list);

            // Sort by album and then random
            list.Sort(MediaHolder.CompareAlbumRandom);

            // Assign special ordinals according to order of tracks on each album
            int ordinal = 1;
            string lastAlbum = string.Empty;
            foreach(MediaHolder item in list)
            {
                if (!string.Equals(item.Album, lastAlbum, StringComparison.Ordinal))
                {
                    ordinal = 1;
                    lastAlbum = item.Album;
                }
                item.SpecialOrdinal = ordinal;
                ++ordinal;
            }

            // Now sort by special ordinal, album, and title
            list.Sort(delegate(MediaHolder a, MediaHolder b)
            {
                int comp = a.SpecialOrdinal - b.SpecialOrdinal;
                if (comp != 0) return comp;
                comp = String.Compare(a.Album, b.Album, StringComparison.Ordinal);
                if (comp != 0) return comp;
                return String.Compare(a.Title, b.Title, StringComparison.Ordinal);
            });

            PostPlayList(list);
        }

        private void FullShuffle_Click(object sender, EventArgs e)
        {
            List<MediaHolder> list = RetrievePlayList();
            list.Sort(delegate(MediaHolder a, MediaHolder b)
            {
                return a.RandomOrdinal - b.RandomOrdinal;
            });
            PostPlayList(list);
        }

        private void AlbumTrack_Click(object sender, EventArgs e)
        {
            List<MediaHolder> list = RetrievePlayList();
            list.Sort(); // Traditional sort order
            PostPlayList(list);
        }


#if DEBUG
        static void Dump(List<MediaHolder> list)
        {
            foreach(MediaHolder item in list)
            {
                Debug.WriteLine("{0} | {1} | {2} | {3} | {4}", item.Track, item.Album, item.Title, item.RandomOrdinal, item.SpecialOrdinal);
            }
            Debug.WriteLine("----------");
        }
#endif
    }


    class MediaHolder : IComparable<MediaHolder>
    {
        static Random s_Rand = new Random();

        WMPLib.IWMPMedia mMedia;

        public MediaHolder(WMPLib.IWMPMedia media)
        {
            mMedia = media;
        }

        public WMPLib.IWMPMedia Media
        {
            get { return mMedia; }
        }

        string m_album;
        public string Album
        {
            get
            {
                if (m_album == null)
                {
                    m_album = mMedia.getItemInfo("WM/AlbumTitle");
                    if (m_album == null) m_album = string.Empty;
                }
                return m_album;
            }
        }

        int m_track;
        public int Track
        {
            get
            {
                if (m_track == 0)
                {
                    string track = mMedia.getItemInfo("WM/TrackNumber");
                    if (track == null) track = string.Empty;
                    if (!int.TryParse(track, out m_track)) m_track = -1;
                }
                return m_track;
            }
        }

        String m_title;
        public String Title
        {
            get
            {
                if (m_title == null)
                {
                    m_title = mMedia.getItemInfo("Title");
                    if (m_title == null) m_title = string.Empty;
                }
                return m_title;
            }
        }

        int m_randomOrdinal = 0;
        public int RandomOrdinal
        {
            get
            {
                if (m_randomOrdinal == 0)
                {
                    m_randomOrdinal = s_Rand.Next();
                }
                return m_randomOrdinal;
            }
        }

        public int SpecialOrdinal { get; set; }

        public bool IsSameAs(MediaHolder other)
        {
            // Windows does some weird rounding. The same track can show up with nearly a second difference in duration
            // So we look for values between -1 and 1
            double diff = mMedia.duration - other.mMedia.duration;

            return diff > -1 && diff < 1
                && Track == other.Track
                && string.Equals(Title, other.Title, StringComparison.Ordinal)
                && string.Equals(Album, other.Album, StringComparison.Ordinal);
        }

        public static int CompareAlbumRandom(MediaHolder a, MediaHolder b)
        {
            int comp = String.Compare(a.Album, b.Album, StringComparison.Ordinal);
            if (comp != 0) return comp;
            return a.RandomOrdinal - b.RandomOrdinal;
        }

        /// <summary>
        /// Default comparer uses the traditional sort order: Album, Track, Title
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MediaHolder other)
        {
            int comp = String.Compare(Album, other.Album, StringComparison.Ordinal);
            if (comp != 0) return comp;
            comp = Track - other.Track;
            if (comp != 0) return comp;
            return String.Compare(Title, other.Title);
        }
    }


}
