   M S B A M L   `   `   `  ����M  IComicSuggestionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ComicSuggestionAppComicSuggestionApp  5   	   #    ComicSuggestionApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ComicSuggestionApp     5   	     � Title$)  #Comic Suggestion コミサジェ！��5   	   $	��720��6)   $
��1280��66   .��5	      � .+�5
      �� $)��#Comic Suggestion コミサジェ！��6
   $
��Left=�6,   $	��Top=�6G   $��
328,10,0,0q�6_   $��48(�6s   �� 5      $'��!どんな漫画が読みたい？��6
   $
��Left=�6    $	��Top=�6;   $��	10,80,0,0q�6S   $��24(�6f   �� 5      -   5         ��Name$ DBStatus��5   
   $,��&現在のデータベース登録数: ��6   $
��Left=�65   $	��Top=�6P   $��	10,10,0,0q�6h   �� 5      -   5         ��Name$ inputTextBox��5      $
��Left=�6"   $	��157��6=    �� TextWrapping$
 Wrap=�6J   $�� ��6^   $	��Top=�6f   $	��921��6~   $��
258,84,0,0q�6�   $��24(�6�   �� 5      -   5         ��Name$ suggestButton��5      $��サジェスト！��6"   $
��Left=�63   $	��Top=�6N   $	��196��6f   $��545,246,0,0q�6r   $��52��6�   $��36(�6�   6�   �� 5      $-��'あなたへのおすすめ漫画は…��6
   $
��Left=�6"   $	��Top=�6=   $��
81,320,0,0q�6U   $��24(�6i   �� 5      -   5        $ resultLabel��5   
   $�� ��6   $
��Left=�6*   $	��Top=�6E   $��160,367,0,0q�6]   $��24(�6r   �� 5      -   5         ��Name$ helpButtton��5   
   $��Resources/help.png��6   $
��Left=�6;   $��30��6V   $	��Top=�6b   $��30��6z   $��1184,84,0,0q�6�   6�   �� 5      -   5        $ addDBButton��5      $��データベース登録��6    $
��Left=�63   $	��Top=�6N   $��86��6f   $��	10,36,0,0q�6q   6�   5      5      