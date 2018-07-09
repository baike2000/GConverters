using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private readonly Dictionary<ConverterEnum, Tuple<MyImageConverter<IMyImage>, List<Control>>> _converters = 
                                        new Dictionary<ConverterEnum, Tuple<MyImageConverter<IMyImage>, List<Control>>>();
        public MainForm()
        {
            InitializeComponent();
            ListLoad();
        }

        private void CreateControls()
        {

        }

        private void ListLoad()
        {
            _converters.Add(ConverterEnum.GrayScale, 
                new Tuple<MyImageConverter<IMyImage>, List<Control>>(
                    new GrayscaleConverter<IMyImage>(), 
                    new List<Control>()));
            _converters.Add(ConverterEnum.GrayWorld,
                new Tuple<MyImageConverter<IMyImage>, List<Control>>(
                    new GrayWorldConverter<IMyImage>(), 
                    new List<Control>()));
            _converters.Add(ConverterEnum.NonLinear,
                new Tuple<MyImageConverter<IMyImage>, List<Control>>(
                    new NonLinearConverter<IMyImage>(), 
                    new List<Control> { txtC, lblC, txtGamma, lblGamma }));
            _converters.Add(ConverterEnum.Logaritm,
                new Tuple<MyImageConverter<IMyImage>, List<Control>>(
                    new LogarithmConverter<IMyImage>(), 
                    new List<Control> { txtC, lblC }));
            _converters.Add(ConverterEnum.Binary,
                new Tuple<MyImageConverter<IMyImage>, List<Control>>(
                    new BinaryConverter<IMyImage>(), 
                    new List<Control> { txtC, lblC }));
            var lst = new List<ListBoxItem>();
            foreach(var conv in _converters)
            {
                lst.Add(new ListBoxItem() {Name = conv.Value.Item1.Name, Value = conv.Value.Item1.ConverterType});
            }
            lstConverts.DataSource = lst;
            lstConverts.DisplayMember = "Name";
            lstConverts.ValueMember = "Value";
            txtC.Visible = false;
            lblC.Visible = false;
            txtGamma.Visible = false;
            lblGamma.Visible = false;
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
                var converter = _converters[(ConverterEnum)item.Value].Item1;
                var dstbitmap = new Bitmap(pbcSource.Image);
                try
                {
                    if (converter is IMyImageConverterWithParams<IMyImage> @default)
                    {
                        if (converter.NumberOfParams == 0)
                        {
                            var dst = @default.Convert(new MyImage(bitmap));
                            dst.ConvertTo(dstbitmap);
                        }
                        if (converter.NumberOfParams == 2)
                        {
                            if (converter.TypeOfParams == typeof(double))
                            {

                                var c = Convert.ToDouble(txtC.Text);
                                var gamma = Convert.ToDouble(txtGamma.Text);
                                var dst = @default.Convert(new MyImage(bitmap), c, gamma);
                                dst.ConvertTo(dstbitmap);
                            }
                        }
                        if (converter.NumberOfParams == 1)
                        {
                            if (converter.TypeOfParams == typeof(double))
                            {
                                var c = Convert.ToDouble(txtC.Text);
                                var dst = @default.Convert(new MyImage(bitmap), c);
                                dst.ConvertTo(dstbitmap);
                            }
                            if (converter.TypeOfParams == typeof(int))
                            {
                                var c = Convert.ToInt32(txtC.Text);
                                var dst = @default.Convert(new MyImage(bitmap), c);
                                dst.ConvertTo(dstbitmap);
                            }
                        }
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
            var converter = _converters[(ConverterEnum)item.Value];
            txtC.Visible = false;
            lblC.Visible = false;
            txtGamma.Visible = false;
            lblGamma.Visible = false;
            foreach (var control in converter.Item2)
            {
                control.Visible = true;
            }
        }
    }
}
