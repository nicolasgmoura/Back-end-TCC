//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMarketing
{
    using System;
    using System.Collections.Generic;
    
    public partial class Denuncia
    {
        public int IdDenuncia { get; set; }
        public int IdConsumidor { get; set; }
        public Nullable<int> IdPublicacao { get; set; }
        public Nullable<int> IdComentario { get; set; }
        public string Mensagem { get; set; }
        public System.DateTime DataDenuncia { get; set; }
    
        public virtual Comentario Comentario { get; set; }
        public virtual Publicacao Publicacao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
