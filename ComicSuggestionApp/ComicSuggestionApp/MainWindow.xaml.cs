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
				MessageBox.Show("データベースの読み込みに失敗しました。");

				Application.Current.Shutdown();
			}

			DBStatus.Content = "現在のデータベース登録数: " + comicDB.Length + "作品";
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
			var inputText = inputTextBox.Text;
			resultLabel.Content = "";
			var keywordWeight = new double[20] {1, 0.8, 0.64, 0.512, 0.4096, 0.32768, 0.262144, 0.2097152, 0.16777216, 0.134217728, 0.107374182, 0.085899346, 0.068719477, 0.054975581, 0.043980465, 0.035184372, 0.028147498, 0.022517998, 0.018014399, 0.014411519 };

			// 各作品ごとの一致率
			var totalWeight = new double[comicDB.Length];

			// 作品ごとの一致率を計算
			for (var i = 0; i < comicDB.Length; i++)
			{
				for (var j = 0; j < comicDB[i].keywords.Length; j++)
				{
					totalWeight[i] += CountString(inputText, comicDB[i].keywords[j]) * keywordWeight[j];
				}
			}

			// 一致度が高い順に最大5作品出力

			// 上位の漫画DB
			var comicRanking = new List<ComicDB>();

			for (var i = 0; i < 5; i++)
			{
				var max = 0.0;
				var maxIndex = -1;

				for (var j = 0; j < comicDB.Length; j++)
				{
					if(max < totalWeight[j])
					{
						max = totalWeight[j];
						maxIndex = j;
					}
				}

				if(maxIndex == -1)
				{
					break;
				}

				comicRanking.Add(comicDB[maxIndex]);
				totalWeight[maxIndex] = -1;
			}



			for (int i = 0; i < comicRanking.Count; i++)
			{
				resultLabel.Content += (i + 1).ToString() + "位！ " + comicRanking[i].title + "\n";
			}
		}

		/// <summary>
		/// 指定した文字列の中に含まれるキーワードの数をカウントします。
		/// </summary>
		/// <param name="target">検索対象文字列</param>
		/// <param name="keyword">検索キーワード</param>
		/// <returns>含まれるキーワード数</returns>
		private static int CountString(string target, string keyword)
		{
			return (target.Length - target.Replace(keyword, "").Length) / keyword.Length;
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

			for (var i = 0; i < dbStrings.Length; i++)
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
