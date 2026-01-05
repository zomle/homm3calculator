using Homm3.WPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Homm3.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string SupportedHotaVersion = "1.8.0";

		public List<Monster> Monsters { get; set; }
		public List<MapObject> MapObjects { get; set; }
		public List<MonsterStrengthZone> MonsterStrengthZones { get; set; }
		public List<MonsterStrengthMap> MonsterStrengthMaps { get; set; }
		public List<MonsterSize> MonsterSizes { get; set; }
		public List<Preset> Presets { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			MonsterStrengthZones = new List<MonsterStrengthZone>
			{
				new MonsterStrengthZone(ZoneStrength.ZoneGuard, 0),
				new MonsterStrengthZone(ZoneStrength.Weak, -1),
				new MonsterStrengthZone(ZoneStrength.Average, 0),
				new MonsterStrengthZone(ZoneStrength.Strong, 1)
			};

			MonsterStrengthMaps = new List<MonsterStrengthMap>
			{
				new MonsterStrengthMap("Weak", 2),
				new MonsterStrengthMap("Normal", 3),
				new MonsterStrengthMap("Strong", 4)
			};

			MonsterSizes = new List<MonsterSize>
			{
				new MonsterSize(" -- Range -- "),
				new MonsterSize("Few", 1, 4),
				new MonsterSize("Several", 5, 9),
				new MonsterSize("Pack", 10, 19),
				new MonsterSize("Lots", 20, 49),
				new MonsterSize("Horde", 50, 99),
				new MonsterSize("Throng", 100, 249),
				new MonsterSize("Swarm", 250, 499),
				new MonsterSize("Zounds", 500, 999),
				new MonsterSize("Legion", 1000, 4000),
				new MonsterSize(" -- Exact number -- "),
			};
			for (int i = 1; i <= 120; ++i)
			{
				MonsterSizes.Add(new MonsterSize(i));
			}

			Presets = LoadPresets(true);

			Monsters = LoadMonsters();
			MapObjects = LoadMapObjects();

			Loaded += MainWindow_Loaded;

			cmbPreset.SelectionChanged += CmbPreset_SelectionChanged;
			btnReloadPresets.Click += BtnReloadPresets_Click;
			btnRegeneratePresets.Click += BtnRegeneratePresets_Click;
			btnIncWeek.Click += BtnIncWeek_Click;

			txtWeek.TextChanged += TxtWeekCount_TextChanged;

			cmbMonster.SelectionChanged += CmbSelectionChanged;
			cmbMapObject1.SelectionChanged += CmbSelectionChanged;
			cmbMapObject2.SelectionChanged += CmbSelectionChanged;
			cmbMapObject3.SelectionChanged += CmbSelectionChanged;
			cmbMapObject4.SelectionChanged += CmbSelectionChanged;
			cmbMapObject5.SelectionChanged += CmbSelectionChanged;
			cmbMapObject6.SelectionChanged += CmbSelectionChanged;

			btnClearMapObject1.Click += BtnClearMapObject1_Click;
			btnClearMapObject2.Click += BtnClearMapObject2_Click;
			btnClearMapObject3.Click += BtnClearMapObject3_Click;
			btnClearMapObject4.Click += BtnClearMapObject4_Click;
			btnClearMapObject5.Click += BtnClearMapObject5_Click;
			btnClearMapObject6.Click += BtnClearMapObject6_Click;
			btnClearGuard.Click += BtnClearGuard_Click;
			btnRandomGuard.Click += BtnRandomGuard_Click;

			cmbMonsterStrengthZone.SelectionChanged += CmbSelectionChanged;
			cmbMonsterStrengthMap.SelectionChanged += CmbSelectionChanged;
			cmbMonsterSize.SelectionChanged += CmbSelectionChanged;

			txtZoneConnection.TextChanged += TxtZoneConnection_TextChanged;
			txtZoneTotalCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneBulwarkCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneCastleCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneConfluxCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneCoveCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneDungeonCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneFactoryCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneFortressCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneInfernoCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneNecropolisCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneRampartCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneStrongholdCount.TextChanged += TxtZoneCount_TextChanged;
			txtZoneTowerCount.TextChanged += TxtZoneCount_TextChanged;
		}

		private void BtnIncWeek_Click(object sender, RoutedEventArgs e)
		{
			int tmpInt = 0;
			int.TryParse(txtWeek.Text, out tmpInt);
			txtWeek.Text = (tmpInt + 1).ToString();
			RefreshUi();
		}

		private string GetPresetFilePath()
		{
			var rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			return Path.Combine(rootDir, "Presets.json");
		}

		private void BtnRegeneratePresets_Click(object sender, RoutedEventArgs e)
		{
			var presetPath = GetPresetFilePath();
			if (File.Exists(presetPath))
			{
				try
				{
					File.Move(presetPath, presetPath + "." + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
				}
				catch(Exception ex)
				{
					MessageBox.Show("Failed to rename existing Presets.json file: " + ex.Message, "Error with presets", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}

			try
			{
				File.WriteAllText(presetPath, WPF.Resources.Presets);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Failed to generate new Presets.json file: " + ex.Message, "Error with presets", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			Presets = LoadPresets();
			cmbPreset.ItemsSource = Presets;
		}

		private void BtnReloadPresets_Click(object sender, RoutedEventArgs e)
		{
			Presets = LoadPresets();
			cmbPreset.ItemsSource = Presets;
		}

		private void ClearFilteredComboBox(FilteredComboBox filteredComboBox)
		{
			filteredComboBox.Text = string.Empty;
			filteredComboBox.SelectedIndex = -1;
			RefreshUi();
		}

		private void BtnRandomGuard_Click(object sender, RoutedEventArgs e)
		{
			var userInput = GetUserInput();
			var result = Calculator.GenerateRandomGuard(userInput);

			if (result == null)
			{
				lblResult.DataContext = $"Couldn't find any possible random guard for the selected criteria.";
			}
			else
			{
				lblResult.DataContext = $"Possible random guard for the selection: <b>{result.AverageMonsterCount}</b> <b>{result.Monster.DisplayName}</b> on week 1.";
			}

		}

		private void BtnClearGuard_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMonster);
		}

		private void BtnClearMapObject1_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject1);
		}

		private void BtnClearMapObject2_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject2);
		}

		private void BtnClearMapObject3_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject3);
		}

		private void BtnClearMapObject4_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject4);
		}

		private void BtnClearMapObject5_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject5);
		}

		private void BtnClearMapObject6_Click(object sender, RoutedEventArgs e)
		{
			ClearFilteredComboBox(cmbMapObject6);
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{

			var version = GetType().Assembly.GetName().Version;
			Title = $"HOMM3 HotA ({SupportedHotaVersion}) Calculator {version.Major}.{version.Minor}.{version.Build}";

			cmbPreset.ItemsSource = Presets;
			cmbMonsterSize.ItemsSource = MonsterSizes;
			cmbMonster.ItemsSource = Monsters;
			cmbMapObject1.ItemsSource = MapObjects.ToList();
			cmbMapObject2.ItemsSource = MapObjects.ToList();
			cmbMapObject3.ItemsSource = MapObjects.ToList();
			cmbMapObject4.ItemsSource = MapObjects.ToList();
			cmbMapObject5.ItemsSource = MapObjects.ToList();
			cmbMapObject6.ItemsSource = MapObjects.ToList();

			cmbMonsterStrengthZone.ItemsSource = MonsterStrengthZones;
			cmbMonsterStrengthZone.SelectedIndex = 1;

			cmbMonsterStrengthMap.ItemsSource = MonsterStrengthMaps;
			cmbMonsterStrengthMap.SelectedIndex = 2;

			cmbMonsterSize.SelectedIndex = 1;

			txtWeek.Text = "1";

			txtZoneTotalCount.Text = "2";
			txtZoneBulwarkCount.Text = "0";
			txtZoneCastleCount.Text = "0";
			txtZoneConfluxCount.Text = "0";
			txtZoneCoveCount.Text = "0";
			txtZoneDungeonCount.Text = "0";
			txtZoneFactoryCount.Text = "0";
			txtZoneFortressCount.Text = "0";
			txtZoneInfernoCount.Text = "0";
			txtZoneNecropolisCount.Text = "0";
			txtZoneRampartCount.Text = "0";
			txtZoneStrongholdCount.Text = "0";
			txtZoneTowerCount.Text = "0";
			
			RefreshUi();
		}

		private void CmbPreset_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			RefreshSelections();
		}

		private void TxtWeekCount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			RefreshUi();
		}

		private void TxtZoneCount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			RefreshUi();
		}

		private void TxtZoneConnection_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			RefreshUi();
		}

		private void CmbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			RefreshUi();
		}

		private void NumberValidationTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			e.Handled = !IsTextAllowed(e.Text);
		}

		private static readonly Regex _regex = new Regex("[^0-9]+");
		private static bool IsTextAllowed(string text)
		{
			return !_regex.IsMatch(text);
		}

		// Use the DataObject.Pasting Handler 
		private void NumberValidationTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				string text = (string)e.DataObject.GetData(typeof(string));
				if (!IsTextAllowed(text))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		private UserInput GetUserInput()
		{
			var userInput = new UserInput();
			userInput.SelectedMonster = cmbMonster.SelectedItem as Monster;
			userInput.SelectedMapObjects[1] = cmbMapObject1.SelectedItem as MapObject;
			userInput.SelectedMapObjects[2] = cmbMapObject2.SelectedItem as MapObject;
			userInput.SelectedMapObjects[3] = cmbMapObject3.SelectedItem as MapObject;
			userInput.SelectedMapObjects[4] = cmbMapObject4.SelectedItem as MapObject;
			userInput.SelectedMapObjects[5] = cmbMapObject5.SelectedItem as MapObject;
			userInput.SelectedMapObjects[6] = cmbMapObject6.SelectedItem as MapObject;
			userInput.SelectedMonsterStrengthMap = cmbMonsterStrengthMap.SelectedItem as MonsterStrengthMap;
			userInput.SelectedMonsterStrengthZone = cmbMonsterStrengthZone.SelectedItem as MonsterStrengthZone;
			userInput.SelectedMonsterSize = cmbMonsterSize.SelectedItem as MonsterSize;
			int tmpInt = 0;
			int.TryParse(txtWeek.Text, out tmpInt);
			userInput.CurrentWeek = Math.Max(tmpInt, 1);

			int.TryParse(txtZoneConnection.Text, out tmpInt);
			userInput.ZoneConnectionValue = tmpInt;

			int.TryParse(txtZoneTotalCount.Text, out tmpInt);
			userInput.TotalTownZoneCount = tmpInt;

			int.TryParse(txtZoneBulwarkCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Bulwark] = tmpInt;

			int.TryParse(txtZoneCastleCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Castle] = tmpInt;

			int.TryParse(txtZoneRampartCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Rampart] = tmpInt;

			int.TryParse(txtZoneTowerCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Tower] = tmpInt;

			int.TryParse(txtZoneInfernoCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Inferno] = tmpInt;

			int.TryParse(txtZoneNecropolisCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Necropolis] = tmpInt;

			int.TryParse(txtZoneDungeonCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Dungeon] = tmpInt;

			int.TryParse(txtZoneStrongholdCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Stronghold] = tmpInt;

			int.TryParse(txtZoneFortressCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Fortress] = tmpInt;

			int.TryParse(txtZoneConfluxCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Conflux] = tmpInt;

			int.TryParse(txtZoneCoveCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Cove] = tmpInt;

			int.TryParse(txtZoneFactoryCount.Text, out tmpInt);
			userInput.TownZoneCounts[Town.Factory] = tmpInt;

			return userInput;
		}

		private void RefreshUi()
		{
			var userInput = GetUserInput();

			grdZoneGuard.IsEnabled = userInput.IsZoneGuard;
			grdMapObjects.IsEnabled = !userInput.IsZoneGuard;
			//userInput.HasDwellingMapObject();
			string monsterAiValue = string.Empty;
			if (userInput.HasUnknownMapObject()) 
			{
				cmbMonsterSize.Visibility =  Visibility.Visible;
				if (userInput.SelectedMonster != null && userInput.SelectedMonsterSize != null)
				{
					int aiValue = userInput.SelectedMonster?.AiValue ?? 0;
					int min = aiValue * userInput.SelectedMonsterSize.MinValue;
					int max = aiValue * userInput.SelectedMonsterSize.MaxValue;
					monsterAiValue = min == max ? $"{min}" : $"{min}-{max}";
				}
				lblTotalAiValueLabel.Content = "";
				//lblTotalAiValueLabel.ToolTip = "Possible AI value of the map object on week 1";
			}
			else
			{
				cmbMonsterSize.Visibility = Visibility.Collapsed;
				if (userInput.SelectedMonster != null)
				{
					monsterAiValue = (userInput.SelectedMonster?.AiValue ?? 0).ToString();
				}
				lblTotalAiValueLabel.Content = "Total AI Value";
				//lblTotalAiValueLabel.ToolTip = "Total AI value of the map objects.";
			}
			lblMonsterAiValue.Content = monsterAiValue;

			var values = Calculator.CalculateValues(userInput);

			lblMapObject1AiValue.Content = values.AiValues.Object1Value == -1 ? "???" : values.AiValues.Object1Value.ToString();
			lblMapObject2AiValue.Content = values.AiValues.Object2Value == -1 ? "???" : values.AiValues.Object2Value.ToString();
			lblMapObject3AiValue.Content = values.AiValues.Object3Value == -1 ? "???" : values.AiValues.Object3Value.ToString();
			lblMapObject4AiValue.Content = values.AiValues.Object4Value == -1 ? "???" : values.AiValues.Object4Value.ToString();
			lblMapObject5AiValue.Content = values.AiValues.Object5Value == -1 ? "???" : values.AiValues.Object5Value.ToString();
			lblMapObject6AiValue.Content = values.AiValues.Object6Value == -1 ? "???" : values.AiValues.Object6Value.ToString();
			lblZoneConnectionAiValue.Content = values.AiValues.ZoneConnectionValue;
			lblProtectionIndex.Content = values.ProtectionIndex;

			if (userInput.SelectedMonster == null)
			{
				return;
			}

			string resultMessage;
			if (userInput.HasUnknownMapObject())
			{
				lblTotalAiValue.Content = "";

				var objs = userInput.GetUnknownMapObjects();
				if (objs.Count > 1)
				{
					resultMessage = "Can't guess more than 1 unknown map objects :(";
				}
				else
				{
					var obj = objs.Single();
					var possibleObjectDict = Calculator.GuessPossibleObjects(userInput);
					
					var sb = new StringBuilder();
					if (possibleObjectDict.Count > 0)
					{
						foreach (var item in possibleObjectDict)
						{
							if (!string.IsNullOrWhiteSpace(item.Key))
							{
								sb.Append($"<b>{item.Key}</b>: {string.Join(", ", item.Value)}; ");
							}
							else
							{
								sb.Append($"{string.Join(", ", item.Value)}; ");
							}
						}
					} 
					else
					{
						sb.Append("<b>none?</b> ");
					}

					if (!string.IsNullOrWhiteSpace(obj.Warning))
					{
						sb.Append($"({obj.Warning})");
					}

					resultMessage = $"Possible <b>{obj.CommonObjectPrefix}</b> types: {sb.ToString()}";
				}
			} 
			else
			{
				lblTotalAiValue.Content = values.AiValues.TotalAiValue;

				if (values.AiValues.TotalAiValue < 2000)
				{
					resultMessage = $"There should be 0 <b>{userInput.SelectedMonster.Name}</b>. (AI value is too low for guards)";
				}
				else
				{
					if (values.MonsterValues.AverageMonsterCount == 0 || values.MonsterValues.AverageMonsterCount < Math.Floor(((float)(values.MonsterValues.MinimalCount + values.MonsterValues.MaximalCount)) / 2) || values.MonsterValues.AverageMonsterCount >= 100)
					{
						resultMessage = $"There supposed to be an average <b>{values.MonsterValues.AverageMonsterCount}</b> <b>{userInput.SelectedMonster.Name}</b> on week 1, but this monster shouldn't be generated with these settings.";

					}
					else
					{
						int averageAdjustedForWeek = Calculator.AdjustForWeeklyGrowth(values.MonsterValues.AverageMonsterCount, userInput.CurrentWeek);
						if (values.MonsterValues.MonsterCountDeviation == 0)
						{
							resultMessage = $"There will be <b>{averageAdjustedForWeek}</b> <b>{userInput.SelectedMonster.Name}</b> on week <b>{userInput.CurrentWeek}</b>.";
						}
						else
						{
							resultMessage = $"There will be <b>{averageAdjustedForWeek - values.MonsterValues.MonsterCountDeviation}-{averageAdjustedForWeek + values.MonsterValues.MonsterCountDeviation}</b> ({averageAdjustedForWeek}+-{values.MonsterValues.MonsterCountDeviation}) <b>{userInput.SelectedMonster.Name}</b> on week <b>{userInput.CurrentWeek}</b>.";
						}
					}
				}
			}

			lblResult.DataContext = resultMessage;
		}
		
		private void RefreshSelections()
		{
			var preset = cmbPreset.SelectedItem as Preset;
			if (preset == null || string.IsNullOrEmpty(preset.Name))
			{
				return;
			}

			cmbMonsterStrengthZone.SelectedIndex = MonsterStrengthZones.FindIndex(e => e.Name == preset.ZoneMonsterStrength);
			if (preset.ZoneConnectionValue != null)
			{ 
				txtZoneConnection.Text = preset.ZoneConnectionValue.ToString();
			}

			if (preset.NumberOfZonesWithTowns != null)
			{
				txtZoneTotalCount.Text = preset.NumberOfZonesWithTowns.ToString();
			}
			RefreshUi();
		}

		private List<Preset> LoadPresets(bool silent = false)
		{
			var result = new List<Preset>
			{
				new Preset() { Name = "" },
			};

			Settings settings;

			var presetPath = GetPresetFilePath();
			if (File.Exists(presetPath))
			{
				string content;
				try
				{
					content = File.ReadAllText(presetPath);
				}
				catch (Exception e)
				{
					MessageBox.Show("Failed to load presets.json file: " + e.Message, "Error with presets", MessageBoxButton.OK, MessageBoxImage.Error);
					return result;
				}

				try
				{
					settings = JsonConvert.DeserializeObject<Settings>(content);
				}
				catch (Exception e)
				{
					MessageBox.Show("Failed to process presets.json file: " + e.Message, "Error with presets", MessageBoxButton.OK, MessageBoxImage.Error);
					return result;
				}

				result.AddRange(settings.Presets);
			}
			else
			{
				if (!silent)
				{
					MessageBox.Show("Couldn't find Presets.json in the current folder: " + presetPath + Environment.NewLine + "You can regenerate Presets.json using the other button.", "Presets.json not found", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			return result;
		}

		private static List<Monster> LoadMonsters()
		{
			return MonsterFactory.ListMonsters();
		}

		private static List<MapObject> LoadMapObjects()
		{
			return MapObjectFactory.ListMapObjects();
		}
	}
}
