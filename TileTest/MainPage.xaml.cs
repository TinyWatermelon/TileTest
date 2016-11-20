using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TileTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {                                               //xml hint-style  title subtitle body captionsubtle
                                                    //      类型        大     中      小       灰色（默认大小）
        private const string TileTemplateXml = @"           
        <tile branding='name'>
	        <visual version='3'>
		        <binding template='TileMedium'>
			         <group>
				        <subgroup>
					        <text hin-style='title'>{0}</text>
					        <text hint-wrap='true'>{1}</text>
				        </subgroup>
			        </group>
		        </binding>
		        <binding template='TileWide'>
			        <group>
				        <subgroup hint-weight='60'>
					            <text hint-style='title'>{0}</text>
					            <text hint-stle='body' hint-wrap='true'>{1}</text>
					            <text hint-stle='body' hint-wrap='true'>{2}</text>
				        </subgroup>
				        <subgroup hint-weight='40'>
                                <text >{0}</text>
					            <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
                                <text >{0}</text>
					            <text hint-style='captionsubtle' hint-wrap='true'>{2}</text>
				        </subgroup>
			        </group>
		        </binding>
		        <binding template='TileLarge'>
			        <group>
				        <subgroup>
					            <text hint-style='title'>{0}</text>
					            <text hint-stle='body' hint-wrap='true'>{1}</text>
					            <text hint-stle='body' hint-wrap='true'>{2}</text>
				        </subgroup>
				        <subgroup>
                                <text hint-style='title'>{0}</text>
					            <text hint-stle='body' hint-wrap='true'>{1}</text>
					            <text hint-stle='body' hint-wrap='true'>{2}</text>
				        </subgroup>
			        </group>
                    <group>
				        <subgroup>
					            <text hint-style='title'>{0}</text>
					            <text hint-stle='body' hint-wrap='true'>{1}</text>
					            <text hint-stle='body' hint-wrap='true'>{2}</text>
				        </subgroup>
				        <subgroup>
                                <text hint-style='title'>{0}</text>
					            <text hint-stle='body' hint-wrap='true'>{1}</text>
					            <text hint-stle='body' hint-wrap='true'>{2}</text>
				        </subgroup>
			        </group>
		        </binding>
	        </visual>
        </tile>
        ";
        private int times = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void but0_Click(object sender, RoutedEventArgs e)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueueForSquare150x150(true);
            updater.EnableNotificationQueueForSquare310x310(true);
            updater.EnableNotificationQueueForWide310x150(true);
            updater.EnableNotificationQueue(true);
            
            //updater.Clear();
             
            var doc = new XmlDocument();
            var xml = string.Format(TileTemplateXml, "课程", "教师", "时间地点", "Monday");
            doc.LoadXml(WebUtility.HtmlDecode(xml),new XmlLoadSettings { ProhibitDtd=false,ValidateOnParse=false,ElementContentWhiteSpace=false,ResolveExternals=false });
                
            updater.Update(new TileNotification(doc));
        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Clear();
        }

        private async void but2_Click(object sender, RoutedEventArgs e)
        {
            var tileid = "tile";
            var displayName = "SecondaryTile";
            var args = string.Format("Click@{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            var logourl = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            var size = TileSize.Square150x150;
            var secondaryTile = new SecondaryTile(tileid, displayName, args, logourl, size);
            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            bool pinned = await secondaryTile.RequestCreateAsync();
        }

        private async void but3_Click(object sender, RoutedEventArgs e)
        {
            var secondaryTile = new SecondaryTile("tile");
            bool pinned = await secondaryTile.RequestDeleteAsync();
        }

        private void but4_Click(object sender, RoutedEventArgs e)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForSecondaryTile("tile");
            updater.EnableNotificationQueueForSquare150x150(true);
            updater.EnableNotificationQueueForSquare310x310(true);
            updater.EnableNotificationQueueForWide310x150(true);
            updater.EnableNotificationQueue(true);

            var doc = new XmlDocument();
            var xml = string.Format(TileTemplateXml, "You have updated " + (++times).ToString() + " times!", DateTime.Now.ToString());
            doc.LoadXml(WebUtility.HtmlDecode(xml), new XmlLoadSettings { ProhibitDtd = false, ValidateOnParse = false, ElementContentWhiteSpace = false, ResolveExternals = false });

            updater.Update(new TileNotification(doc));
        }
        private void but5_Click(object sender, RoutedEventArgs e)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForSecondaryTile("tile");
            updater.Clear();
        }
    }
}
