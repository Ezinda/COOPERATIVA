using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public class ParserIngresoTabaco
    {
        public char PlaceholderCharacter = '?';

        public char ErrorCharacter = '!';

        public bool Force = false;

        public bool AutoInsertSpaces = true;

        public void ImprimirArchivoEncabezados(List<EncabezadoIngresoTabaco> encabezados, string path)
        {
            File.Create(path).Close();

            using (var writer = new System.IO.StreamWriter(path, false, System.Text.UTF8Encoding.UTF8))
            {
                var fieldsandnames = GetFields(typeof(EncabezadoIngresoTabaco));

                fieldsandnames = fieldsandnames.OrderBy(x => x.Field.Position).ToList();

                var linecounter = 1;

                foreach (var renglon in encabezados)
                {
                    var line = String.Empty;
                    var fieldsonline = GetFields(renglon);
                    var carretposition = 0;

                    foreach (var item in fieldsandnames)
                    {
                        var name = item.Name;
                        var size = item.Field.Size;
                        var formatter = item.Field.Formatter;
                        var type = item.Field.Type;
                        var fillzero = item.Field.FillWithZero;
                        var toupper = item.Field.ToUpper;
                        var cut = item.Field.Cut;
                        var value = fieldsonline.Where(x => x.Name == name).Select(x => x.Field.Value).Single() ?? String.Empty;

                        if (toupper)
                        {
                            value = value.ToUpper();
                        }

                        if (Force)
                        {
                            if (value.Length < size)
                            {
                                value += new String(PlaceholderCharacter, size - value.Length);
                            }
                            else if (value.Length > size)
                            {
                                value = new String(ErrorCharacter, size);
                            }
                        }
                        else if (AutoInsertSpaces)
                        {
                            if (value.Length < size)
                            {
                                if (fillzero == 1)
                                {
                                    value += new String('0', size - value.Length);
                                }
                                else if (fillzero == -1)
                                {
                                    value = new String('0', size - value.Length) + value;
                                }
                                else
                                {
                                    value += new String(' ', size - value.Length);
                                }
                            }
                            else if (value.Length > size && cut)
                            {
                                value = value.Substring(0, size);
                            }
                            else if (value.Length > size)
                            {
                                throw new Exception(String.Format("Member: {0}, Value: {1} - Value must be of size {2}", name, value, size));
                            }
                        }
                        else
                        {
                            if (value.Length > size)
                            {
                                value = value.Substring(0, size);
                            }
                            if (value.Length < size)
                            {
                                throw new Exception(String.Format("Member: {0}, Value: {1} - Value must be of size {2}", name, value, size));
                            }
                        }

                        carretposition += size;
                        line += value;
                    }

                    writer.WriteLine(line);

                    linecounter++;
                }
            }
        }

        public void ImprimirArchivoRenglones(List<RenglonIngresoTabaco> renglones, string path)
        {
            File.Create(path).Close();

            using (var writer = new System.IO.StreamWriter(path, false, System.Text.UTF8Encoding.UTF8))
            {
                var fieldsandnames = GetFields(typeof(RenglonIngresoTabaco));

                fieldsandnames = fieldsandnames.OrderBy(x => x.Field.Position).ToList();

                var linecounter = 1;

                foreach (var renglon in renglones)
                {
                    var line = String.Empty;
                    var fieldsonline = GetFields(renglon);
                    var carretposition = 0;
                    
                    foreach (var item in fieldsandnames)
                    {
                        var name = item.Name;
                        var size = item.Field.Size;
                        var formatter = item.Field.Formatter;
                        var type = item.Field.Type;
                        var fillzero = item.Field.FillWithZero;
                        var value = fieldsonline.Where(x => x.Name == name).Select(x => x.Field.Value).Single() ?? String.Empty;

                        if (Force)
                        {
                            if (value.Length < size)
                            {
                                value += new String(PlaceholderCharacter, size - value.Length);
                            }
                            else if (value.Length > size)
                            {
                                value = new String(ErrorCharacter, size);
                            }
                        }
                        else if (AutoInsertSpaces)
                        {
                            if (value.Length < size)
                            {
                                if (fillzero == 1)
                                {
                                    value += new String('0', size - value.Length);
                                }
                                else if (fillzero == -1)
                                {
                                    value = new String('0', size - value.Length) + value;
                                }
                                else
                                {
                                    value += new String(' ', size - value.Length);
                                }
                            }
                            else if (value.Length > size)
                            {
                                throw new Exception(String.Format("Member: {0}, Value: {1} - Value must be of size {2}", name, value, size));
                            }
                        }
                        else
                        {
                            if (value.Length < size || value.Length > size)
                            {
                                throw new Exception(String.Format("Member: {0}, Value: {1} - Value must be of size {2}", name, value, size));
                            }
                        }

                        carretposition += size;
                        line += value;
                    }

                    writer.WriteLine(line);

                    linecounter++;
                }
            }
        }

        public void ImprimirArchivoEncabezados(string path)
        {
            ImprimirArchivoEncabezados(path, System.Console.Out);
        }

        public void ImprimirArchivoEncabezados(string path, System.IO.TextWriter outputstream)
        {
            using (var reader = new System.IO.StreamReader(path))
            {
                var fieldsandnames = GetFields(typeof(EncabezadoIngresoTabaco));

                fieldsandnames = fieldsandnames.OrderBy(x => x.Field.Position).ToList();

                outputstream.WriteLine(String.Format("File properties:"));
                outputstream.WriteLine(String.Format("- Name: {0}", System.IO.Path.GetFileName(path)));
                outputstream.WriteLine(String.Format("- FullName: {0}", System.IO.Path.GetFullPath(path)));
                outputstream.WriteLine(String.Format("- Encoding: {0}", reader.CurrentEncoding));
                outputstream.WriteLine();
                outputstream.WriteLine();
                outputstream.WriteLine(String.Format("Posicion - Tipo - Longitud - Nombre - Valor"));

                var linecounter = 1;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var carretposition = 0;

                    outputstream.WriteLine("Line: " + linecounter);

                    foreach (var item in fieldsandnames)
                    {
                        var name = item.Name;
                        var field = item.Field;
                        var value = line.Substring(carretposition, field.Size);
                        carretposition += field.Size;

                        var format = String.Format("{0,2} - {1,3} - {2,2} - {3,50} - {4}", field.Position, field.Type.Name.Substring(0, 3), field.Size, name, value);
                        outputstream.WriteLine(format);
                    }

                    linecounter++;
                    outputstream.WriteLine();
                }
            }
        }

        public void ImprimirArchivoRenglones(string path)
        {
            ImprimirArchivoRenglones(path, System.Console.Out);
        }

        public void ImprimirArchivoRenglones(string path, System.IO.TextWriter outputstream)
        {
            using (var reader = new System.IO.StreamReader(path))
            {
                var fieldsandnames = GetFields(typeof(RenglonIngresoTabaco));

                fieldsandnames = fieldsandnames.OrderBy(x => x.Field.Position).ToList();

                outputstream.WriteLine(String.Format("File properties:"));
                outputstream.WriteLine(String.Format("- Name: {0}", System.IO.Path.GetFileName(path)));
                outputstream.WriteLine(String.Format("- FullName: {0}", System.IO.Path.GetFullPath(path)));
                outputstream.WriteLine(String.Format("- Encoding: {0}", reader.CurrentEncoding));
                outputstream.WriteLine();
                outputstream.WriteLine();
                outputstream.WriteLine(String.Format("Posicion - Tipo - Longitud - Nombre - Valor"));

                var linecounter = 1;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var carretposition = 0;

                    outputstream.WriteLine("Line: " + linecounter);

                    foreach (var item in fieldsandnames)
                    {
                        var name = item.Name;
                        var field = item.Field;

                        bool incomplete = false;
                        string value;

                        try
                        {
                            value = line.Substring(carretposition, field.Size);
                        }
                        catch
                        {
                            value = line.Substring(carretposition);
                            incomplete = true;
                        }

                        carretposition += field.Size;

                        var format = String.Format("{0,2} - {1,3} - {2,2} - {3,50} - {4}", field.Position, field.Type.Name.Substring(0, 3), field.Size, name, value);
                        outputstream.WriteLine(format);

                        if (incomplete)
                        {
                            outputstream.WriteLine("^");
                            outputstream.WriteLine("|");
                            outputstream.WriteLine("--- Longitud de registro incompleta - Se esperaban " + field.Size + " caracteres se obtuvieron " + value.Length + "caracteres.");

                            break;
                        }
                    }

                    linecounter++;
                    outputstream.WriteLine();
                }
            }
        }

        private List<FieldAndName> GetFields(object obj)
        {
            var list = new List<FieldAndName>();
            object instance;

            if (obj is Type)
            {
                instance = Activator.CreateInstance(obj as Type);
            }
            else
            {
                instance = obj;
            }

            var fields = instance.GetType().GetFields();

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(CampoIngresoTabaco))
                {
                    var name = field.Name;
                    var value = field.GetValue(instance) as CampoIngresoTabaco;

                    var fieldandname = new FieldAndName()
                    {
                        Name = name,
                        Field = value
                    };

                    list.Add(fieldandname);
                }
            }

            return list;
        }

        class FieldAndName
        {
            public string Name { get; set; }

            public CampoIngresoTabaco Field { get; set; }
        }
    }

    public class EncabezadoIngresoTabaco
    {
        public CampoIngresoTabaco CodigoDepositoAcopiador        = new CampoIngresoTabaco(01 , typeof(Int64)     , 04, -1);
        public CampoIngresoTabaco CuitAdquirienteTabaco          = new CampoIngresoTabaco(02 , typeof(Int64)     , 11);
        public CampoIngresoTabaco RazonSocialAdquirientetabaco   = new CampoIngresoTabaco(03 , typeof(String)    , 30, 0, true, true);
        public CampoIngresoTabaco CuitProductor                  = new CampoIngresoTabaco(04 , typeof(Int64)     , 11);
                                                                                                                 
        public CampoIngresoTabaco RazonSocialProductor           = new CampoIngresoTabaco(05 , typeof(String)    , 30, 0, true, true);
        public CampoIngresoTabaco Calle                          = new CampoIngresoTabaco(06 , typeof(String)    , 30, 0, true, true);
        public CampoIngresoTabaco NumeroPuerta                   = new CampoIngresoTabaco(07 , typeof(String)    , 06);
        public CampoIngresoTabaco Piso                           = new CampoIngresoTabaco(08 , typeof(String)    , 05);
                                                                                                                 
        public CampoIngresoTabaco OficinaDptoLocal               = new CampoIngresoTabaco(09 , typeof(String)    , 05);
        public CampoIngresoTabaco Sector                         = new CampoIngresoTabaco(10 , typeof(String)    , 05);
        public CampoIngresoTabaco Torre                          = new CampoIngresoTabaco(11 , typeof(String)    , 05);
        public CampoIngresoTabaco Manzana                        = new CampoIngresoTabaco(12 , typeof(String)    , 05);
                                                                                                                 
        public CampoIngresoTabaco CodigoPostal                   = new CampoIngresoTabaco(13 , typeof(String)    , 08);
        public CampoIngresoTabaco Localidad                      = new CampoIngresoTabaco(14 , typeof(String)    , 30, 0, true, true);
        public CampoIngresoTabaco CodigoDeProvincia              = new CampoIngresoTabaco(15 , typeof(Int64)     , 02);
        public CampoIngresoTabaco CodigoDeProvinciaTabaco        = new CampoIngresoTabaco(16 , typeof(Int64)     , 02);
                                                                                                                 
        public CampoIngresoTabaco LocalidadTabaco                = new CampoIngresoTabaco(17 , typeof(String)    , 30, 0, true, true);
        public CampoIngresoTabaco FechaRomaneo                   = new CampoIngresoTabaco(18 , typeof(DateTime)  , 08, new DateFormatter());
        public CampoIngresoTabaco NumeroRomaneo                  = new CampoIngresoTabaco(19 , typeof(Int64)     , 12, -1);
        public CampoIngresoTabaco VariedadTabaco                 = new CampoIngresoTabaco(20 , typeof(String)    , 02);
                                                                                                                 
        public CampoIngresoTabaco PuntoDeVentaFacturaLiquidacion = new CampoIngresoTabaco(21 , typeof(Int64)     , 04, -1);
        public CampoIngresoTabaco NumeroFacturaLiquidacion       = new CampoIngresoTabaco(22 , typeof(Int64)     , 08, -1);
        public CampoIngresoTabaco TipoComprobante                = new CampoIngresoTabaco(23 , typeof(Int64)     , 03, -1);
        public CampoIngresoTabaco NumeroDespachoImportacion      = new CampoIngresoTabaco(24 , typeof(String)    , 16);
                                                                                                                 
        public CampoIngresoTabaco FechaFacturaLiquidacionDI      = new CampoIngresoTabaco(25 , typeof(DateTime)  , 08, new DateFormatter());
        public CampoIngresoTabaco EmisorComprobante              = new CampoIngresoTabaco(26 , typeof(Int64)     , 01);
        public CampoIngresoTabaco ImporteNetoGravado             = new CampoIngresoTabaco(27 , typeof(Decimal)   , 11, new DecimalFormatter(9, 2), -1);
        public CampoIngresoTabaco CAI                            = new CampoIngresoTabaco(28 , typeof(Int64)     , 14, -1);
                                                                                                                 
        public CampoIngresoTabaco TipoOperacion                  = new CampoIngresoTabaco(29 , typeof(Int64)     , 01);
    }                                                                                                            
                                                                                                                 
    public class RenglonIngresoTabaco                                                                                   
    {                                                                                                            
        public CampoIngresoTabaco NumeroRomaneo                  = new CampoIngresoTabaco(01 , typeof(Int64)     , 12, -1);
        public CampoIngresoTabaco Clase                          = new CampoIngresoTabaco(02 , typeof(String)    , 04);
        public CampoIngresoTabaco PesoFardoEnKilos               = new CampoIngresoTabaco(03 , typeof(Decimal)   , 05, new DecimalFormatter(), -1);
        public CampoIngresoTabaco CodigoTrazabilidadInterno      = new CampoIngresoTabaco(04 , typeof(Int64)     , 30, -1);
    }

    public class CampoIngresoTabaco
    {
        public CampoIngresoTabaco(int position, Type type, int size)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = null;
            FillWithZero = 0;
        }

        public CampoIngresoTabaco(int position, Type type, int size, int zero)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = null;
            FillWithZero = zero;
        }

        public CampoIngresoTabaco(int position, Type type, int size, int zero, bool toupper, bool cut)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = null;
            FillWithZero = zero;
            ToUpper = toupper;
            Cut = cut;
        }

        public CampoIngresoTabaco(int position, Type type, int size, IFormatter formatter)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = formatter;
            FillWithZero = 0;
        }

        public CampoIngresoTabaco(int position, Type type, int size, IFormatter formatter, int zero)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = formatter;
            FillWithZero = zero;
        }

        public CampoIngresoTabaco(int position, Type type, int size, string value)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = value;
            Formatter = null;
            FillWithZero = 0;
        }

        public CampoIngresoTabaco(int position, Type type, int size, string value, int zero)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = value;
            Formatter = null;
            FillWithZero = zero;
        }

        public int Position { get; private set; }

        public Type Type { get; private set; }

        public int Size { get; private set; }

        public string Value { get; set; }

        public IFormatter Formatter { get; private set; }

        private int _fillWithZero;
        public int FillWithZero
        {
            get
            {
                return _fillWithZero;
            }
            private set
            {
                if (value < -1 || value > 1)
                {
                    throw new Exception("FillWithZero must be -1 for Left Zeros, 0 to disable, or 1 for Right Zeros");
                }

                _fillWithZero = value;
            }
        }

        public bool ToUpper { get; private set; }

        public bool Cut { get; private set; }
    }

    public class DecimalFormatter : IFormatter
    {
        private int _parteEntera = 3;
        private int _parteDecimal = 2;

        public DecimalFormatter()
            : this(3, 2)
        {
        }

        public DecimalFormatter(int parteEntera, int parteDecimal)
        {
            _parteEntera = parteEntera;
            _parteDecimal = parteDecimal;
        }

        public string GetFormattedValue(object realValue)
        {
            var rounded = Math.Round((decimal)realValue, 2);
            var enteros = (int)rounded;
            var decimales = (int)((rounded - Math.Floor(rounded)) * 100m);
            
            string strenteros = String.Empty;

            if (enteros.ToString().Length <= _parteEntera)
            {
                //strenteros = new String('0', _parteEntera - enteros.ToString().Length) + enteros.ToString();
                strenteros = enteros.ToString();
            }
            else
            {
                throw new Exception("Parte entera mayor de " + _parteEntera + " digitos");
            }

            string strdecimales = String.Empty;

            if (decimales.ToString().Length <= _parteDecimal)
            {
                strdecimales = decimales.ToString() + new String('0', _parteDecimal - decimales.ToString().Length);
            }
            else
            {
                throw new Exception("Parte decimal mayor de " + _parteDecimal + " digitos");
            }

            return strenteros + strdecimales;
        }

        public object GetRealValue(string formattedvalue)
        {
            return decimal.Parse(formattedvalue) / 100;
        }
    }

    public class DateFormatter : IFormatter
    {
        public string GetFormattedValue(object realValue)
        {
            return ((DateTime)realValue).ToString("yyyyMMdd");
        }

        public object GetRealValue(string formattedvalue)
        {
            return DateTime.ParseExact(formattedvalue,
                "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None);
        }
    }

    public interface IFormatter
    {
        object GetRealValue(string formattedvalue);

        string GetFormattedValue(object realvalue);
    }
}
