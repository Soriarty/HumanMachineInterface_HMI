﻿#pragma checksum "..\..\..\Assets\ManualPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CAB9469A39813E152762E1AFCAE2A533"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Dragablz;
using HM_Interface_Visu.Assets;
using MahApps.Metro.Controls;
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


namespace HM_Interface_Visu.Assets {
    
    
    /// <summary>
    /// ManualPage
    /// </summary>
    public partial class ManualPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAxis;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPneumathic;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMFU;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOther;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle AxisIndicator;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle PneumathiclIndicator;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle MFUIndicator;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle OtherIndicator;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Assets\ManualPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel ManualPageSlot;
        
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
            System.Uri resourceLocater = new System.Uri("/HM_Interface_Visu;component/assets/manualpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Assets\ManualPage.xaml"
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
            this.btnAxis = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Assets\ManualPage.xaml"
            this.btnAxis.Click += new System.Windows.RoutedEventHandler(this.btnMenu_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnPneumathic = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Assets\ManualPage.xaml"
            this.btnPneumathic.Click += new System.Windows.RoutedEventHandler(this.btnMenu_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMFU = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Assets\ManualPage.xaml"
            this.btnMFU.Click += new System.Windows.RoutedEventHandler(this.btnMenu_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnOther = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Assets\ManualPage.xaml"
            this.btnOther.Click += new System.Windows.RoutedEventHandler(this.btnMenu_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AxisIndicator = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.PneumathiclIndicator = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 7:
            this.MFUIndicator = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 8:
            this.OtherIndicator = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 9:
            this.ManualPageSlot = ((System.Windows.Controls.DockPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

