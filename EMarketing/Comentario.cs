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
    
    public partial class Comentario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comentario()
        {
            this.Comentario1 = new HashSet<Comentario>();
            this.Denuncia = new HashSet<Denuncia>();
        }
    
        public int IdComentario { get; set; }
        public int IdPublicacao { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> IdResposta { get; set; }
        public System.DateTime DataComentario { get; set; }
    
        public virtual Publicacao Publicacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentario1 { get; set; }
        public virtual Comentario Comentario2 { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
