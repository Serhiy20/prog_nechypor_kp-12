﻿#pragma checksum "..\..\FirstWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "769558535C5CD53ED008433374621F124209F9B396D15DA5DFBB18D59F6E3645"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Lab1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Lab1 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Lab1.MainWindow MainForm;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToWin2;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToWin3;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToWin4;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToWin5;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/Lab1;component/firstwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FirstWindow.xaml"
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
            this.MainForm = ((Lab1.MainWindow)(target));
            return;
            case 2:
            this.ToWin2 = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\FirstWindow.xaml"
            this.ToWin2.Click += new System.Windows.RoutedEventHandler(this.ToWin2_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ToWin3 = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\FirstWindow.xaml"
            this.ToWin3.Click += new System.Windows.RoutedEventHandler(this.ToWin3_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ToWin4 = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\FirstWindow.xaml"
            this.ToWin4.Click += new System.Windows.RoutedEventHandler(this.ToWin4_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ToWin5 = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\FirstWindow.xaml"
            this.ToWin5.Click += new System.Windows.RoutedEventHandler(this.ToWin5_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ExitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\FirstWindow.xaml"
            this.ExitBtn.Click += new System.Windows.RoutedEventHandler(this.ExitBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
