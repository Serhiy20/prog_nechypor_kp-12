   M S B A M L   `   `   `  ����D  @TermPaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TermPaper	TermPaper  5   	       TermPaper.GamesWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:TermPaper     5   	     � Title$  GamesWindow��5   	   $	��450��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6[    � Icon$ icon.png��6q   .��5	      � .+�5
   
   �� -   5         ��Name$
 Exit��5
      $(��"До головного вікна��6   $
��Left=�6<   $��
635,10,0,0q�6W   $	��Top=�6k   $	��145��6�   $��30��6�   $��Times New Roman)�6�   $��16(�6�   �� 5   
   -   5         ��Name$ CBClub��5      $
��Left=�6#   $��	10,38,0,0q�6>   $	��Top=�6Q   $	��174��6i   �� 5   
   -   5        $ 	CBStadium��5      $
��Left=�6&   $��
189,38,0,0q�6A   $	��Top=�6U   $	��203��6m   �� 5   
   -   5        $ CBDate��5      $
��Left=�6#   $��
397,38,0,0q�6>   $	��Top=�6R   $	��184��6j    J� RenderTransformOrigin$ 0.348,0.655&�6v   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $	��321��6@   $��	10,89,0,0q�6M   $	��Top=�6`   $	��770��6x   �� 5   
   -   5        $ Button1��5      $��Обрати��6"   $
��Left=�63   $��
109,65,0,0q�6N   $	��Top=�6b   $��75��6z   $��Times New Roman)�6�   $��14(�6�   �� 5   
   -   5        $ Button2��5      $��Обрати��6"   $
��Left=�63   $��
317,65,0,0q�6N   $	��Top=�6b   $��75��6z   $��Times New Roman)�6�   $��14(�6�   �� 5   
   -   5        $ Button3��5      $��Обрати��6"   $
��Left=�63   $��
506,65,0,0q�6N   $	��Top=�6b   $��75��6z   $��Times New Roman)�6�   $��14(�6�   �� 5   
   -	   5        $ Button4��5      $7��1Вивести повний графік ігор��6"   $
��Left=�6G   $��
604,54,0,0q�6b   $	��Top=�6v   $	��176��6�   $��30��6�   $��Times New Roman)�6�   $��14(�6�   �� 5   
   $+��%Вивести матчі клубу:��6   $
��Left=�6/   $��10,7,0,0q�6J   $	��Top=�6\   $	��174��6t   $��Center=�6�   �� 5   
   $6��0Вивесті матчі на стадіоні:��6   $
��Left=�65   $��	189,7,0,0q�6P   $	��Top=�6c   $	��203��6{   $��Center=�6�   �� 5   
   $;��5Вивести матчі в обраний день:��6   $
��Left=�68   $��	397,7,0,0q�6S   $	��Top=�6f   $	��184��6~   $��Center=�6�   5      5      