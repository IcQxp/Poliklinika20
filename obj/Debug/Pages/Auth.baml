   M S B A M L   `   `   `  ����H  DPoliklinika20, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Poliklinika20.PagesPoliklinika20.Pages  5          Poliklinika20.Pages.Auth   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Poliklinika20.Pages     5        N� Title$
  Auth��5	      .��5      � -   5        .+�5   
   �� $��Center=�6   $��Center=�62   .+�5   
   �� 
Логин5      �� 5      -   5         ��Name$ Login��5      .��6%   5      �� 5   
   Пароль5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �  � ColumnDefinitions 5      �� 5      $��* �6+   �� 5      $��90 �6+   .+�5      G� -   5         G�Name$ Password��5   "   $	��230��64   �� 5      -   5        $ 
PasswordTB��5      $	��230��62   $y�Hidden=�6>   �� 5       -   5         ��Name$ ShowPwd��5       $��Показать��6.   $��2��6A   6Q   5!      5"      �� 5%      -   5         ��Name$ CaptchaContainer��5%   ^   $��2��6   $	��160��6&   $��2��63   $ Vertical=�6G   $y�	Collapsed=�6x   .+�5&      �� $ 
Horizontal=�6   $��Center=�66   $��Center=�6Q   $	��100��6n   .+�5'      � .+�5(      �� -   5        $��SymnolsPanel��5(   J   $	��250��6%   $ 
Horizontal=�61   $��Center=�6^   $��Center=�6{   �� 5*      -   5        $��NoiseCanvas��5*   !   5+      �� 5,      -	   5        $��70��5,      $��Обновить��6?   ��5-      �� 5.       �� 
TargetType" ��6$   .�5/   "   ��  �� Property � ��	 �� Value
	 ���� 6?   �� 50   "    � ��$	 0q�6D   51      54      55      �� 56      -
   5        $ 
CaptchaBox��56      $	��300��6.   $��0 10 0 0q�6:   59      �� 5;      -   5        $��90��5;      $��30��6    $��5 5 5 5q�6,   .��6T   
Войти5=      5>      5?      5@      