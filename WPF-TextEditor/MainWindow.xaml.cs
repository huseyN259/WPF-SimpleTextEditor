using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WPF_TextEditor;

#nullable disable

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void btnOpenFile_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "All files|*.*|Text files|*.txt";
		openFileDialog.FilterIndex = 2;

		if (openFileDialog.ShowDialog() == true)
			using (StreamReader sr = new StreamReader(openFileDialog.FileName))
				TextBox.Text = sr.ReadToEnd();
	}

	private void btnSaveFile_Click(object sender, RoutedEventArgs e)
	{
		SaveFileDialog save = new SaveFileDialog();
		save.FileName = "Huseyn.txt";
		save.Filter = "Text File | *.txt";

		if (string.IsNullOrWhiteSpace(FilePath.Text))
		{
			if (save.ShowDialog() == true)
			{
				StreamWriter writer = new StreamWriter(save.OpenFile());
				writer.WriteLine(TextBox.Text);
				FilePath.Text = save.FileName;
				writer.Dispose();
				writer.Close();

				MessageBox.Show("File Succesfully Saved...", "", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		else 
			File.WriteAllText(FilePath.Text, TextBox.Text);
	}
	private void btnTextboxProcess_Click(object sender, RoutedEventArgs e)
	{
		Button button = (sender) as Button;

		if (button.Content.ToString() == "Cut") 
			TextBox.Cut();
		else if (button.Content.ToString() == "Copy") 
			TextBox.Copy();
		else if (button.Content.ToString() == "Paste") 
			TextBox.Paste();
		else if (button.Content.ToString() == "Select All") 
			TextBox.SelectAll();

		TextBox.Focus();
	}

	private bool AutoSave;
	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (AutoSave == true) 
			btnSaveFile_Click(null, null);
	}

	private void ToggleButtonAutoSave_Click(object sender, RoutedEventArgs e)
	{
		if (ToggleButtonAutoSave.IsChecked == true)
		{
			if (!string.IsNullOrWhiteSpace(FilePath.Text)) 
				AutoSave = true;
			else
			{
				MessageBox.Show("Determine Your File...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				AutoSave = false;
				ToggleButtonAutoSave.IsChecked = false;
			}
		}
	}
}