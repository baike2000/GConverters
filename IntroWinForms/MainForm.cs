using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Converters.Exceptions;
using Converters.Image;
using Converters.ImageConverter;
using ConvertInterfaces;
using ConvertInterfaces.Enum;

namespace IntroWinForms
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<ConverterEnum, IMyImageConverterWithParams<MyImage>> _converters = 
                                        new Dictionary<ConverterEnum, IMyImageConverterWithParams<MyImage>>();
        private IMyImageConverterWithParams<MyImage> _currentConverter = null;
        public MainForm()
        {
            InitializeComponent();
            ListLoad();
        }

        private void CreateControls(IMyImageConverterWithParams<MyImage> converter)
        {
            for (int i = 0; i < converter.NumberOfParams; i++)
            {
                var lbl = new Label
                {
                    Text = converter.ParamNames[i],
                    Name = "lbl" + converter.ParamNames[i],
                    Visible = false
                };
                var txtb = new TextBox
                {
                    Name = "txt" + converter.ParamNames[i],
                    Text = "",
                    Visible = false
                };
                converter.Controls.AddRange(new Control[] {lbl, txtb});
            }

            PutControlsToPanel(converter.Controls.Select(x => x as Control).ToList());
        }

        private void PutControlsToPanel(List<Control> controls)
        {
            var x = 0;
            var y = 0;
            foreach (var control in controls)
            {
                control.Left = x + 5;
                control.Top = y;
                control.Width = 70;
                control.Height = 12;
                pnlControls.Controls.Add(control);
                y += 17;
            }
        }

        private void ListLoad()
        {
            var pluginAssembly = Assembly.GetAssembly(typeof(MyImageConverter<MyImage>));
            var converters =
                (from tp in pluginAssembly.DefinedTypes
                    from intr in tp.ImplementedInterfaces
                    where intr.ToString() == typeof(IMyImageConverterWithParams<>).ToString()
                    select tp).Cast<Type>().ToList();
            foreach (Type converterType in converters)
            {
                var t = converterType.MakeGenericType(typeof(MyImage));
                if (Activator.CreateInstance(t) is IMyImageConverterWithParams<MyImage> converter)
                {
                    CreateControls(converter);
                    _converters.Add(converter.ConverterType, converter);
                }
            }

            var lst = new List<ListBoxItem>();
            foreach (var conv in _converters)
            {
                lst.Add(new ListBoxItem
                {
                    Name = conv.Value.Name,
                    Value = conv.Value.ConverterType
                });
            }

            lstConverts.DataSource = lst;
            lstConverts.DisplayMember = "Name";
            lstConverts.ValueMember = "Value";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbcSource.Load(openFileDialog.FileName);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (lstConverts.SelectedIndex < 0)
            {
                MessageBox.Show("Надо выбрать конвертер");
                return;
            }

            if (pbcSource.Image == null)
            {
                MessageBox.Show("Надо выбрать изображение");
                return;
            }

            using (Bitmap bitmap = new Bitmap(pbcSource.Image))
            {
                var item = lstConverts.SelectedItem as ListBoxItem;
                var converter = _converters[(ConverterEnum)item.Value];
                var dstbitmap = new Bitmap(pbcSource.Image);
                try
                {
                    if (converter is IMyImageConverterWithParams<MyImage> @default)
                    {
                        var cparams = new object[@default.NumberOfParams];
                        int cnt = 0;
                        foreach (var param in @default.Controls)
                        {
                            if ((param is Control control))
                            {
                                if (!control.Name.StartsWith("lbl"))
                                {
                                    cparams[cnt] = Convert.ChangeType(control.Text, @default.TypeOfParams);
                                    cnt++;
                                }
                            }
                        }
                        var img = @default.Convert(new MyImage(bitmap), cparams);
                        img.ConvertTo(dstbitmap);
                    }
                    pbxDest.Image?.Dispose();
                    pbxDest.Image = dstbitmap;
                }
                catch (NumberOfArgsException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BoundException<int> ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BoundException<double> ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (pbxDest.Image != dstbitmap)
                        dstbitmap.Dispose();
                }
            }
        }

        private void lstConverts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lstConverts.SelectedItem as ListBoxItem;
            var converter = _converters[(ConverterEnum) item.Value];
            if (_currentConverter != null)
            {
                foreach (var control in _currentConverter.Controls)
                {
                    if (control is Control ctrl)
                        ctrl.Visible = false;
                }
            }

            _currentConverter = converter;
            foreach (var control in converter.Controls)
            {
                if (control is Control ctrl)
                    ctrl.Visible = true;
            }
        }
    }
}
