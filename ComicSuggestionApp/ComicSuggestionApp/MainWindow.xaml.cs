using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ComicSuggestionApp
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			if (!LoadDatabase())
			{
				MessageBox.Show( "データベースの読み込みに失敗しました。");

				Application.Current.Shutdown();
			}

			DBStatus.Content = "現在のデータベース登録数: " + comicDB.Length;
		}

		/// <summary>
		/// 漫画データベース
		/// </summary>
		struct ComicDB
		{
			/// <summary>
			/// 漫画タイトル
			/// </summary>
			public string title;

			/// <summary>
			/// キーワード
			/// </summary>
			public string[] keywords;
		}


		/// <summary>
		/// 漫画データベース
		/// </summary>
		ComicDB[] comicDB;

		/// <summary>
		/// サジェストボタンをクリックされたときのイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void suggestButton_Click(object sender, RoutedEventArgs e)
		{

		}


		/// <summary>
		/// データベースを読み込みます。
		/// </summary>
		/// <returns>true:成功，false:失敗</returns>
		private bool LoadDatabase()
		{
			var dbStrings = ReadStrings("ComicDB.db");
			if (dbStrings == null)
			{
				return false;
			}

			var cDBList = new List<ComicDB>();

			var cDB = new ComicDB();

			var cDBkeyword = new List<string>();

			for (int i = 0; i < dbStrings.Length; i++)
			{
				// null = 1作品の終わり、DBに登録
				if(dbStrings[i] == "")
				{
					cDB.keywords = cDBkeyword.ToArray();
					cDBList.Add(cDB);

					cDB = new ComicDB();
					cDBkeyword.Clear();
				}
				else
				{
					// "・"が一番最初にある = タイトル
					if (dbStrings[i].IndexOf('・') == 0)
					{
						cDB.title = dbStrings[i].Substring(1);
					}
					else
					{
						cDBkeyword.Add(dbStrings[i]);
					}
				}
			}

			// 最後の作品の処理
			cDB.keywords = cDBkeyword.ToArray();
			cDBList.Add(cDB);

			comicDB = cDBList.ToArray();

			return true;
		}


		/// <summary>
		/// テキストファイルを読み込み配列を返します。
		/// </summary>
		/// <param name="FilePath">読み込むファイル名</param>
		/// <returns>ファイルの内容</returns>
		private string[] ReadStrings(string FilePath)
		{
			// ファイルが存在しない場合、nullを返す。
			if (!File.Exists(FilePath))
			{
				return null;
			}
			using (var file = new StreamReader(FilePath))
			{
				var line = "";
				var list = new List<string>();
				while ((line = file.ReadLine()) != null)
				{
					list.Add(line);
				}

				return list.ToArray();
			}
		}
	}
}
