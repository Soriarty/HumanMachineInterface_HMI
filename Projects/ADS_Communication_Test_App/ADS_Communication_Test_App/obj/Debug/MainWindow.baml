   M S B A M L   `   `   `  ����U  QADS_Communication_Test_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ADS_Communication_Test_AppADS_Communication_Test_App  5   	   +    %ADS_Communication_Test_App.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ADS_Communication_Test_App     5   	   -   5          � Title$  
MainWindow��5   	   $	��595��6   $	��660��6)   .��5	      � $��0,0,2,0q�6    � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� -   5        $��
btnConnect��5      $��Connect��6$   $
��Left=�66   $��10,141,0,10q�6Q   $��Bottom=�6f   $	��180��6�   $��70��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ btnDisconnect��5      $��
Disconnect��6)   $
��Left=�6>   $��210,0,10,10q�6Y   $��Bottom=�6n   $	��180��6�   $��70��6�   $��Center=�6�   � 5   
   -   5        $��
pbProgress��5      $��35��6(   $��	10,15,0,0q�64   $	��Top=�6G   $	��630��6_   $
��Left=�6k   �� 5   
   -   5        $��lbStatus��5      $��1��6    $��110,20,85,0q�6,   $	��Top=�6A   $��Center=�6Y   $��Center=�6v   �� 5   
   -   5        $��lblist��5      $	��385��6!   $��10,0,10,100q�6.   $��Bottom=�6C   $	��Top=�6^   $
��Left=�6}   $	��382��6�   �� 5   
   -   5        $��tbSpeedStep��5      $�� ��6%   $��35��6-   $��414,308,0,0q�69    �� TextWrapping$
 Wrap=�6N   $	��Top=�6b   $	��130��6z   $��Center=�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   $��	SpeedStep��6   $
��Left=�6$   $��549,308,0,0q�6?   $	��Top=�6T   $��35��6l   $��94��6x   $��Center=�6�   �� 5   
   -   5        $��tbSpeed��5      $��35��6!   $��414,349,0,0q�6-   $
 Wrap=�6B   $�� ��6V   $	��Top=�6^   $	��130��6v   $��Center=�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   $��Speed��6   $
��Left=�6    $��549,349,0,0q�6;   $	��Top=�6P   $��35��6h   $��94��6t   $��Center=�6   �� 5   
   -	   5        $��
tbMaxCount��5      $��35��6$   $��414,389,0,0q�60   $
 Wrap=�6E   $�� ��6Y   $	��Top=�6a   $	��130��6y   $��Center=�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   $��MaxCount��6   $
��Left=�6#   $��549,389,0,0q�6>   $	��Top=�6S   $��35��6k   $��94��6w   $��Center=�6�   �� 5   
   -
   5        $��tbIncrement��5      $��35��6%   $��414,429,0,0q�61   $
 Wrap=�6F   $�� ��6Z   $	��Top=�6b   $	��130��6z   $��Center=�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   $��	Increment��6   $
��Left=�6$   $��549,429,0,0q�6?   $	��Top=�6T   $��35��6l   $��94��6x   $��Center=�6�   �� 5   
   -   5        $ btnSetParameter��5      $��Set Parameter��6+   $
��Left=�6C   $��
410,0,0,10q�6^   $��Bottom=�6r   $	��230��6�   $��70��6�   $��Center=�6�   {�.  6�   �� 5   
   -   5        $ btnReadParameter��5      $��Read Parameter��6,   $
��Left=�6E   $��410,0,0,415q�6`   $��Bottom=�6u   $	��230��6�   $��70��6�   $��Center=�6�   {�.  6�   �� 5   
   -   5        $��colorBit��5      ���	#FFF4F4F5$
��Left=�66   $��65��6Q   $��410,154,0,0q�6]   
���   �$	��Top=�6�   $	��230��6�   6�   5      5       