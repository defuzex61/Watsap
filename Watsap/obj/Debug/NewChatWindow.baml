   M S B A M L   `   `   `  ����A  =Watsap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WatsapWatsap  5   	       Watsap.NewChatWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Watsap     5   	     � Title$  NewChatWindow��5   	   $	��450��6   $	��800��6,   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��44* �6   �� 5      $
��339* �6   �� 5      $	��51* �6    � ColumnDefinitions 5   
   �� 5      $
��116* �6   �� 5      $
��118* �6   �� 5      $
��567* �6   .+�5   
   �� $
��Wrap=�6   $��Пользователи��6(   $��10,10,10,10q�6<   $��2��6Q   �� 5   
   -   5         ��Name$ usersLbx��5      $��1��6$   $��10,10,10,10q�6\   $��2��6q   �� 5   
   -   5         ��Name$ logCheckBtn��5      $��2��6&   $)��#Посмотреть историю��66   $��Center=�6S   $��26��6p   $��0,8,0,0q�6|   $	��Top=�6�   $	��548��6�   �� 5   
   -   5        $ exitBtn��5      $��
Выйти��6"   $��Center=�62   $��31��6O   $��2��6[   $��Center=�6h   $��96��6�   6�   �� 5   
   -   5        $ sendBtn��5      $��1��6"   $��Отправить��62   $��31��6F   $��	10,0,10,0q�6R   $��2��6e   $��Center=�6r   6�   �� 5   
   -   5         ��Name$ 	messageTb��5      $��2��6%   $��2��65    �� TextWrapping$
 Wrap=�6B   $'��!Введите сообщение��6V   $��10,10,10,10q�6o   �� 5   
   -   5        $ messagesLbx��5      $��2��6'   $��1��67   $��10,10,10,10q�6o   5      5      