//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopEntities.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vw_Clase
    {
        public System.Guid ID { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<System.Guid> ID_PRODUCTO { get; set; }
        public string COD_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<decimal> PRECIOCOMPRA { get; set; }
        public Nullable<bool> Vigente { get; set; }
    }
}
