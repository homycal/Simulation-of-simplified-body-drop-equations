   M S B A M L   `   `   `  ���� O  KSimulation_equations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Simulation_equationsSimulation_equations      View.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:Simulation_equations       � Title$  
MainWindow��$	��500��$	��800��$	��800��$	��500��.��� -   $��0,0,0,-1q����	#FFF4F4F4 � RowDefinitions �� $
��139* ��� $
��142* ��� $
��149* � � ColumnDefinitions �� �� $	��33* ��� $	��92* ��� $	��33* ��� .+��� -    ��Name$ TextBoxSpeed��$��0,2,10,0q�$��15�� �� TextWrapping$
 Wrap=�$��2��$��2��$��17��$	��Top=�$��Right=�$��60���� -   $ TextBoxAngle��$��	0,31,10,0q�$��30��$
 Wrap=�$��2��$��2��$��18��$	��Top=�$��Right=�$��60���� -   $ TextBoxHeight��$��	0,61,10,0q�$��0��$
 Wrap=�$��2��$��2��$��18��$	��Top=�$��Right=�$��60���� -   $ TextBoxGravity��$��	0,93,10,0q�$
��9.80��$
 Wrap=�$��2��$��2��$��18��$	��Top=�$��Right=�$��60���� $��Right=�$��17��$��0,2,10,0q�$��Initial velocity (m/s)��$
��Wrap=�$	��Top=�$	��105��$��1��$��2��${�False��
���   ��� -   $ TextBoxWeight��$��
0,121,10,0q�$��0��$
 Wrap=�$��2��$��2��$��18��$	��Top=�$��Right=�$��60���� $��Right=�$��	0,32,10,0q�$��
Angle (°)��$
��Wrap=�$	��Top=�$��1��$��2��$��16��$��46���� $��Right=�$��	0,63,10,0q�$��
Height (m)��$
��Wrap=�$	��Top=�$��1��$��2��$��16��$��58���� $��Right=�$��	0,93,10,0q�$��Gravity (m/s²)��$
��Wrap=�$	��Top=�$��1��$��2��$��16��$��73���� $��Right=�$��
0,121,10,0q�$��Weight (Kg)��$
��Wrap=�$	��Top=�$��1��$��2��$��16��$��63���� -    ��Name$ ButtonCalcul��$��Simulate��$
��Left=�$��	10,99,0,0q�$	��Top=�$	��100��$��3��$��2��$��20���� -    ��Name$ CanvasMainGraph��$��
10,7,10,10q�
�������$��2��$��2�� � Cursor$ Cross��.+��� $	��264��$	��350���� -	   $ CanvasEnergy��$��3��
�������$��0,10,0,1q��� -
   $ CanvasSpeed��$��1��
�������$��0,10,0,1q��� -   $ CanvasAcceleration��$��1��
�������$��1��$��	0,10,0,10q��� -    ��Name$ SliderSpeed��$��2��$��10,2,75,133q�$��2��	 �� AutoToolTipPlacement$
	 None=�
 � Maximum$	
 300Q� � Value$ 15Q� � SmallChange$	 0.5Q� � Minimum$ 1Q��� -   $ SliderAngle��$��2��$��10,32,75,103q�$��2��$
 90Q�$ 30Q�$	 0.5Q��� -   $ SliderHeight��$��2��$��10,62,75,71q�$��2��$

 1000Q�$	 0.5Q��� -   $ SliderGravity��$��2��$��10,92,75,43q�$��2��$	 9.8Q�$	 0.5Q�$ 1Q��� -   $ SliderWeight��$��2��$��10,121,75,13q�$��2��$
 50Q�$ 1Q�$	 0.5Q��� $	��0.5q����	#7F000000$��3��$��0,10,0,1q��� $��Energies��$
��Wrap=�$��3��$��0,0,10,0q�$��21��$	��Top=� �� TextAlignment$ Center=�$��14(�$��Right=�$��80�����	#FFDDDDDD�� $	��0.5q�$��3��$��0,1,10,0q�$��Right=�$��80��$��20��$	��Top=����	#7F000000�� $	��0.5q����	#7F000000$��2��$��
10,7,10,10q�$��2���� $��
Trajectory��$
��Wrap=�$��2��$��1��$ Center=�$��16(����	#FFDDDDDD$��Center=�$	��120��$��23��$��Bottom=��� $	��0.5q�$��2��$��1��$��Center=�$	��120�����	#7F000000$��23��$��Bottom=��� $	��0.5q����	#7F000000$��1��$��0,7,0,1q��� $��
Velocities��$
��Wrap=�$��1��$��0,1,10,0q�$��19��$	��Top=�$��Right=�$��72��$��14(�$ Center=����	#FFDDDDDD�� $	��0.5q�$��1��$��0,1,10,0q�$��Right=�$��72��$��19��$	��Top=����	#7F000000�� $	��0.5q����	#7F000000$��1��$��	0,10,0,10q�$��1���� $��Acceleration��$
��Wrap=�$��1��$��0,1,10,0q�$��14(�$ Center=�$��Right=�$��95��$��21��$	��Top=�$��1�����	#FFDDDDDD���� �� $	��0.5q�$��1��$��0,1,10,0q�$��Right=�$��95��$��21��$	��Top=����	#7F000000$��1���� -    ��Name$ TextBlockEquationDesc��$��3��$��1��$
��Wrap=�
�������$��	0,10,0,10q��� $	��0.5q����	#7F000000$��3��$��	0,10,0,10q�$��1���� -   $ TextBoxCoordinates��$��3��$
��Left=�$��	10,31,0,0q�$��2��$��Coordinates:��$
��Wrap=�$	��Top=�$	��145���� -   $ TextBlockError��$��3��$
��Left=�$��
10,122,0,0q�$��2��$
��Wrap=�$	��Top=�$	��140���� $��2��$��180,0,180,10q�$��2��$��Made by Jérémy Holay��$
��Wrap=�$��9(�$ Center=�$��16��$��Bottom=�