using System.Configuration;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using System;

namespace HM_Interface_Visu
{
    class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            HMI_Item = new[]
            {
                new HMI_Item("Home", new Home(),
                    new []
                    {
                        new DocumentationLink(DocumentationLinkType.Wiki, $"{ConfigurationManager.AppSettings["GitHub"]}/wiki", "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    }
                ),
                new HMI_Item("Palette", new PaletteSelector { DataContext = new PaletteSelectorViewModel() },
                    new []
                    {
                        DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                        DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                        DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PaletteHelper>()
                    }),
                new HMI_Item("Buttons & Toggles", new Buttons { DataContext = new ButtonsViewModel() } ,
                    new []
                    {
                        DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                        DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                        DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("Button"),
                        DocumentationLink.StyleLink("CheckBox"),
                        DocumentationLink.StyleLink("PopupBox"),
                        DocumentationLink.StyleLink("ToggleButton"),
                        DocumentationLink.ApiLink<PopupBox>()
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new HMI_Item("Fields", new TextFields(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<TextFields>(),
                        DocumentationLink.StyleLink("TextBox"),
                        DocumentationLink.StyleLink("ComboBox"),
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new HMI_Item("Pickers", new Pickers { DataContext = new PickersViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Pickers>(),
                        DocumentationLink.StyleLink("Clock"),
                        DocumentationLink.StyleLink("DatePicker"),
                        DocumentationLink.ApiLink<TimePicker>()
                    }),
                new HMI_Item("Sliders", new Sliders(), new []
                    {
                        DocumentationLink.DemoPageLink<Sliders>(),
                        DocumentationLink.StyleLink("Sliders")
                    }),
                new HMI_Item("Chips", new Chips(), new []
                    {
                        DocumentationLink.DemoPageLink<Chips>(),
                        DocumentationLink.StyleLink("Chip"),
                        DocumentationLink.ApiLink<Chip>()
                    }),
                new HMI_Item("Typography", new Typography(), new []
                    {
                        DocumentationLink.DemoPageLink<Typography>(),
                        DocumentationLink.StyleLink("TextBlock")
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                        HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new HMI_Item("Cards", new Cards(), new []
                    {
                        DocumentationLink.DemoPageLink<Cards>(),
                        DocumentationLink.StyleLink("Card"),
                        DocumentationLink.ApiLink<Card>()
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new HMI_Item("Icon Pack", new IconPack { DataContext = new IconPackViewModel(snackbarMessageQueue) },
                    new []
                    {
                        DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                        DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PackIcon>()
                    }),
                new HMI_Item("Colour Zones", new ColorZones(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<ColorZones>(),
                        DocumentationLink.ApiLink<ColorZone>()
                    }),
                new HMI_Item("Lists", new Lists { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Lists>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.StyleLink("ListBox"),
                        DocumentationLink.StyleLink("ListView")
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new HMI_Item("Trees", new Trees { DataContext = new TreesViewModel() },
                    new []
                    {
                        DocumentationLink.DemoPageLink<Trees>("Demo View"),
                        DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("TreeView")
                    }),
                new HMI_Item("Grids", new Grids { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Grids>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.StyleLink("DataGrid")
                    }),
                new HMI_Item("Expander", new Expander(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Expander>(),
                        DocumentationLink.StyleLink("Expander")
                    }),
                new HMI_Item("Group Boxes", new GroupBoxes(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<GroupBoxes>(),
                        DocumentationLink.StyleLink("GroupBox")
                    }),
                new HMI_Item("Menus & Tool Bars", new MenusAndToolBars(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                        DocumentationLink.StyleLink("Menu"),
                        DocumentationLink.StyleLink("ToolBar")
                    }),
                new HMI_Item("Progress Indicators", new Progress(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Progress>(),
                        DocumentationLink.StyleLink("ProgressBar")
                    }),
                new HMI_Item("Dialogs", new Dialogs { DataContext = new DialogsViewModel()},
                    new []
                    {
                        DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                        DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                        DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.ApiLink<DialogHost>()
                    }),
                new HMI_Item("Drawer", new Drawers(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Drawers>(),
                        DocumentationLink.ApiLink<DrawerHost>()
                    }),
                new HMI_Item("Snackbar", new Snackbars(),
                    new []
                    {
                        DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                        DocumentationLink.DemoPageLink<Snackbars>(),
                        DocumentationLink.StyleLink("Snackbar"),
                        DocumentationLink.ApiLink<Snackbar>(),
                        DocumentationLink.ApiLink<ISnackbarMessageQueue>()
                    }),
                new HMI_Item("Transitions", new Transitions(),
                    new []
                    {
                        DocumentationLink.WikiLink("Transitions", "Transitions"),
                        DocumentationLink.DemoPageLink<Transitions>(),
                        DocumentationLink.ApiLink<Transitioner>("Transitions"),
                        DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                        DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                    }),
                new HMI_Item("Shadows", new Shadows(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Shadows>(),
                    }),
            };
        }

        public HMI_Item[] DemoItems { get; }

    }
}
