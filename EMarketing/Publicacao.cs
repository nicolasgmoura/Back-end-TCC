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
    
    public partial class Publicacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publicacao()
        {
            this.Comentario = new HashSet<Comentario>();
            this.Denuncia = new HashSet<Denuncia>();
        }
    
        public int IdPublicacao { get; set; }
        public int IdUsuario { get; set; }
        public string ImagemPublicacao { get; set; }
        public System.DateTime DataPublicacao { get; set; }
        public string Titulo { get; set; }
        public string AreaPublicacao { get; set; }
        public int QuantidadeComentarios { get; set; }
        public int QuantidadeDenuncias { get; set; }
        public int QuantidadeVisualizacoes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncia> Denuncia { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}