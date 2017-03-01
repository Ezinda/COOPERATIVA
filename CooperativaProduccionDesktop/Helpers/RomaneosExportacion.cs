using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public class ParserIngresoTabaco
    {
        public static void ImprimirArchivoEncabezado(string path)
        {
            ImprimirArchivoEncabezado(path, System.Console.Out);
        }

        public static void ImprimirArchivoEncabezado(string path, System.IO.TextWriter outputstream)
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

        public static void ImprimirArchivoRenglones(string path)
        {
            ImprimirArchivoRenglones(path, System.Console.Out);
        }

        public static void ImprimirArchivoRenglones(string path, System.IO.TextWriter outputstream)
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

        private static List<FieldAndName> GetFields(Type type)
        {
            var list = new List<FieldAndName>();
            var obj = Activator.CreateInstance(type);
            var fields = obj.GetType().GetFields();

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(CampoIngresoTabaco))
                {
                    var name = field.Name;
                    var value = field.GetValue(obj) as CampoIngresoTabaco;

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
        public CampoIngresoTabaco CodigoDepositoAcopiador        = new CampoIngresoTabaco(01 , typeof(Int64)     , 04);
        public CampoIngresoTabaco CuitAdquirienteTabaco          = new CampoIngresoTabaco(02 , typeof(Int64)     , 11);
        public CampoIngresoTabaco RazonSocialAdquirientetabaco   = new CampoIngresoTabaco(03 , typeof(String)    , 30);
        public CampoIngresoTabaco CuitProductor                  = new CampoIngresoTabaco(04 , typeof(Int64)     , 11);
                                                                                                                 
        public CampoIngresoTabaco RazonSocialProductor           = new CampoIngresoTabaco(05 , typeof(String)    , 30);
        public CampoIngresoTabaco Calle                          = new CampoIngresoTabaco(06 , typeof(String)    , 30);
        public CampoIngresoTabaco NumeroPuerta                   = new CampoIngresoTabaco(07 , typeof(String)    , 06);
        public CampoIngresoTabaco Piso                           = new CampoIngresoTabaco(08 , typeof(String)    , 05);
                                                                                                                 
        public CampoIngresoTabaco OficinaDptoLocal               = new CampoIngresoTabaco(09 , typeof(String)    , 05);
        public CampoIngresoTabaco Sector                         = new CampoIngresoTabaco(10 , typeof(String)    , 05);
        public CampoIngresoTabaco Torre                          = new CampoIngresoTabaco(11 , typeof(String)    , 05);
        public CampoIngresoTabaco Manzana                        = new CampoIngresoTabaco(12 , typeof(String)    , 05);
                                                                                                                 
        public CampoIngresoTabaco CodigoPostal                   = new CampoIngresoTabaco(13 , typeof(String)    , 08);
        public CampoIngresoTabaco Localidad                      = new CampoIngresoTabaco(14 , typeof(String)    , 30);
        public CampoIngresoTabaco CodigoDeProvincia              = new CampoIngresoTabaco(15 , typeof(Int64)     , 02);
        public CampoIngresoTabaco CodigoDeProvinciaTabaco        = new CampoIngresoTabaco(16 , typeof(Int64)     , 02);
                                                                                                                 
        public CampoIngresoTabaco LocalidadTabaco                = new CampoIngresoTabaco(17 , typeof(String)    , 30);
        public CampoIngresoTabaco FechaRomaneo                   = new CampoIngresoTabaco(18 , typeof(DateTime)  , 08, new DateFormatter());
        public CampoIngresoTabaco NumeroRomaneo                  = new CampoIngresoTabaco(19 , typeof(Int64)     , 12);
        public CampoIngresoTabaco VariedadTabaco                 = new CampoIngresoTabaco(20 , typeof(String)    , 02);
                                                                                                                 
        public CampoIngresoTabaco PuntoDeVentaFacturaLiquidacion = new CampoIngresoTabaco(21 , typeof(Int64)     , 04);
        public CampoIngresoTabaco NumeroFacturaLiquidacion       = new CampoIngresoTabaco(22 , typeof(Int64)     , 08);
        public CampoIngresoTabaco TipoComprobante                = new CampoIngresoTabaco(23 , typeof(Int64)     , 03);
        public CampoIngresoTabaco NumeroDespachoImportacion      = new CampoIngresoTabaco(24 , typeof(String)    , 16);
                                                                                                                 
        public CampoIngresoTabaco FechaFacturaLiquidacionDI      = new CampoIngresoTabaco(25 , typeof(DateTime)  , 08, new DateFormatter());
        public CampoIngresoTabaco EmisorComprobante              = new CampoIngresoTabaco(26 , typeof(Int64)     , 01);
        public CampoIngresoTabaco ImporteNetoGravado             = new CampoIngresoTabaco(27 , typeof(Decimal)   , 11, new DecimalFormatter());
        public CampoIngresoTabaco CAI                            = new CampoIngresoTabaco(28 , typeof(Int64)     , 14);
                                                                                                                 
        public CampoIngresoTabaco TipoOperacion                  = new CampoIngresoTabaco(29 , typeof(Int64)     , 01);
    }                                                                                                            
                                                                                                                 
    public class RenglonIngresoTabaco                                                                                   
    {                                                                                                            
        public CampoIngresoTabaco NumeroRomaneo                  = new CampoIngresoTabaco(01 , typeof(Int64)     , 12);
        public CampoIngresoTabaco Clase                          = new CampoIngresoTabaco(02 , typeof(String)    , 04);
        public CampoIngresoTabaco PesoFardoEnKilos               = new CampoIngresoTabaco(03 , typeof(Decimal)   , 05, new DecimalFormatter());
        public CampoIngresoTabaco CodigoTrazabilidadInterno      = new CampoIngresoTabaco(04 , typeof(Int64)     , 30);
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
        }

        public CampoIngresoTabaco(int position, Type type, int size, IFormatter formatter)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = String.Empty;
            Formatter = formatter;
        }

        public CampoIngresoTabaco(int position, Type type, int size, string value)
        {
            Position = position;
            Type = type;
            Size = size;
            Value = value;
            Formatter = null;
        }

        public int Position { get; private set; }

        public Type Type { get; private set; }

        public int Size { get; private set; }

        public string Value { get; set; }

        public IFormatter Formatter { get; set; }
    }

    public class DecimalFormatter : IFormatter
    {
        public string GetFormattedValue(object realValue)
        {
            var rounded = Math.Round((decimal)realValue, 2);
            var enteros = (int)rounded;
            var decimales = (int)((rounded - Math.Floor(rounded)) * 100m);

            return enteros.ToString() + decimales.ToString();
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
