﻿#pragma checksum "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D2BF4A596F099E154529AFA65705F85"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements {
    
    
    /// <summary>
    /// PositionControll
    /// </summary>
    public partial class PositionControll : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPos;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SavePos;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel DataSlot;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PositionGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HM_Interface_Visu;component/assets/manualscreenelements/axisscreenelements/funct" +
                    "ionpanels/positioncontroll.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            ((HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements.PositionControll)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LoadPos = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            this.LoadPos.Click += new System.Windows.RoutedEventHandler(this.LoadPos_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SavePos = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            this.SavePos.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SavePos_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            this.SavePos.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.SavePos_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DataSlot = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 5:
            this.PositionGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 48 "..\..\..\..\..\..\Assets\ManualScreenElements\AxisScreenElements\FunctionPanels\PositionControll.xaml"
            this.PositionGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PositionGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
