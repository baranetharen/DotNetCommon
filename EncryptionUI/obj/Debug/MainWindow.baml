   M S B A M L   `   `   `  ����G  CEncryptionUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:EncryptionUIEncryptionUI  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   S OAESEncryptor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1442741722d33cc3A0clr-namespace:AESEncryptor;assembly=AESEncryptorAESEncryptor 5   	       EncryptionUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:EncryptionUI     5   	   7core&clr-namespace:System;assembly=mscorlib     5   	   G
Encryption0clr-namespace:AESEncryptor;assembly=AESEncryptor     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��833��6)    � 	Resources 5         textboxstyle �� 
TargetType �� Property �� Value��%W   &	        �� 5   
   " ��6%   .�5      ��  � ��$ 12(�6)   66   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   .+�5   
   �� $��
Plain Text��6   $��5q�6&   #��[  61   �� 5   
   -   5        $
��Text��5      $��5,5,0,5q�6A   $��1��6R   $	��351��6c   $
��Left=�6o   ��6   �� *Text+ �� Mode$ TwoWay=�6o   �� 5   
   $��Password��6   $��1��6$   $��5q�61   #��[  6<   G� 5    
   -   5         G�Name$ passbox��5       $��1��6'   $��1��64   $��5q�6D   $	��150��6O   $
��Left=�6[   �� 5!   
   $��2��6   	 �� Orientation$	 Vertical=�6"   .+�5"      �� $��No Of Keys To Generate��6   $��5q�66   #��[  6A   �� 5#      $��Min =0 Max=3��6   $��11(�6+   6;   5$      �� 5%   
   -   5        $��NoKeys��5%      $��2��6!   $��1��6.   $	��150��6>   $
��Left=�6J   $��	5,15,0,16q�6e   .@�5&      �� $��1��6   �� 5'      $��2��6   �� 5(      $��3��6   5)      �� 5*   
   $��Encryption Type��6   $��5q�6+   $��3��67   #��[  6D   �� 5+   
   -   5        $��command��5+      $��3��6#   $	��150��61   $��5q�6>   $��1��6J   $
��Left=�6Z   .@�5,      �� $��From KEY��6   �� 5-      $��To KEY��6   5.      �� 5/   
   $��Generated Keys��6   $��4��6+   $��5q�68   #��[  6C   �� 50   
   -   5        $��tKeys��50      $	��334��6   $��5,5,0,6q�6,   $��4��6>   $��1��6K   $	��150��51      "
 �� VerticalScrollBarVisibility$

 Auto=�6   $
��Left=�6B    �� TextWrapping$ NoWrap=�6]    �� AcceptsReturn . 52      �� 53   
   -   5        $��5��53      $��189,44,189,-29q�6   $��1��66   $	��150��6F   $��Generate��6R   ��54      �� *passwordUpdater+��65   ��  �� ElementName$ passbox��55      57      58      