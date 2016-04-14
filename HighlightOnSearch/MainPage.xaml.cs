using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HighlightOnSearch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<ExampleObject> _objList;
        public MainPage()
        {
            this.InitializeComponent();
            CreateList();
        }

        private async void CreateList()
        {
            Uri appUri = new Uri("ms-appx:///Assets/ExampleData.json");//File name should be prefixed with 'ms-appx:///Assets/*
            StorageFile anjFile = await StorageFile.GetFileFromApplicationUriAsync(appUri);
            _objList = JsonConvert.DeserializeObject<List<ExampleObject>>(await FileIO.ReadTextAsync(anjFile));//Call to helper file for getting the details
            ListOfSomething.ItemsSource = new ObservableCollection<ExampleObject>(_objList);
        }


        //Search is case sensitive but you can easily transform in insensitive
        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                var toSearch = textBox.Text;
                if (ListOfSomething.Items != null)
                {
                    foreach (ExampleObject item in ListOfSomething.Items)
                    {

                          HighlightArticle(item, toSearch); 

                    }
                }
            }
        }

        private void HighlightArticle(ExampleObject actualObj, string toSearch)
        {

            ListViewItem listViewItem = (ListViewItem)ListOfSomething.ContainerFromItem(actualObj);
            UserControl userControl = (UserControl)listViewItem.ContentTemplateRoot;
            var grid = (Grid)userControl.Content;
            var stackPanel = (StackPanel)grid.Children[0];
            // Get the StackPanel's child TextBox


            var indexRichTextBlock = (RichTextBlock)stackPanel.Children[0];
            var aboutRichTextBlock = (RichTextBlock)stackPanel.Children[1];
            listViewItem.Visibility = Visibility.Visible;//usefull when you change the search


            indexRichTextBlock.Blocks.Clear();
            var index_creator = new ListIndexCreator();
            var indexTxt = index_creator.Convert(actualObj, typeof(ExampleObject), null, "it-IT").ToString();
            var splitted_index = indexTxt.Split(new String[] { toSearch }, StringSplitOptions.None);
            var index_par = ManageSplitted(splitted_index, toSearch, indexRichTextBlock);


            aboutRichTextBlock.Blocks.Clear();
            var about_creator = new ListAboutCreator();
            var aboutTxt = about_creator.Convert(actualObj, typeof(ExampleObject), null, "it-IT").ToString();
            var splitted_about = aboutTxt.Split(new String[] { toSearch }, StringSplitOptions.None);

            var about_par = ManageSplitted(splitted_about, toSearch, aboutRichTextBlock);

            if (splitted_index.Length == 1 && splitted_about.Length == 1)
            {
                listViewItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                indexRichTextBlock.Blocks.Add(index_par);
                aboutRichTextBlock.Blocks.Add(about_par);
            }
        }

        private Paragraph ManageSplitted(String[] splitted, string toSearch, RichTextBlock richTextBox)
        {
            Paragraph paragraph = new Paragraph();
            for (int index = 0; index < splitted.Length; index++)
            {
                var piece = splitted[index];
                Run run = new Run();
                run.Text = piece;
                paragraph.Inlines.Add(run);
                if (index < splitted.Length - 1)
                {
                    CreateHighlightedParagraph(paragraph, toSearch, richTextBox);
                }
            }
            return paragraph;
        }

        private void CreateHighlightedParagraph(Paragraph paragraph, string textToHighlight, RichTextBlock textBlock)
        {
            InlineUIContainer cont = new InlineUIContainer();
            var border = new Border();
            border.MinWidth = textBlock.FontSize / 3.5;//when you search whitespace
            border.Background = new SolidColorBrush(Colors.Yellow);
            var text = new TextBlock();
            text.Text = textToHighlight;
            var margin = textBlock.FontSize * (3.0 / 14.0) + 1.0;//find in empiric way
            text.Margin = new Thickness(0.0, 0.0, 0.0, -margin);
            border.Child = text;
            cont.Child = border;
            paragraph.Inlines.Add(cont);
        }


    }
}
