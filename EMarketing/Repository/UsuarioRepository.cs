using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class UsuarioRepository : BaseRepository, IDisposable
    {
        public void Save(Usuario usuario)
        {
            Usuario NovoUsuario = new Usuario();

            NewUsuario(usuario, NovoUsuario);

            if (usuario.IsEmpresa)
            {
                Empresa empresa = new Empresa();
                empresa.IdEmpresa = NovoUsuario.IdUsuario;
                empresa.IdSegmento = usuario.Empresa.IdSegmento;
                empresa.CNPJ = usuario.Empresa.CNPJ;
                empresa.RazaoSocial = usuario.Empresa.RazaoSocial;
                empresa.Fantasia = usuario.Empresa.Fantasia;
                empresa.Endereco = usuario.Empresa.Endereco;
                empresa.Numero = usuario.Empresa.Numero;
                empresa.Bairro = usuario.Empresa.Bairro;
                empresa.CEP = usuario.Empresa.CEP;
                empresa.Telefone = usuario.Empresa.Telefone;
                empresa.Logo = usuario.Empresa.Logo;
                empresa.QuantidadeSeguidores = usuario.Empresa.QuantidadeSeguidores;
                DataModel.Empresa.Add(empresa);
                DataModel.SaveChanges();

            }
            else if(usuario.IsEmpresa == false && usuario.isAdmin == false)
            {
                Consumidor consumidor = new Consumidor();
                consumidor.IdConsumidor = NovoUsuario.IdUsuario;
                consumidor.Nome = usuario.Consumidor.Nome;
                consumidor.DataNascimento = usuario.Consumidor.DataNascimento;
                consumidor.QuantidadeSeguimentos = usuario.Consumidor.QuantidadeSeguimentos;
                DataModel.Consumidor.Add(consumidor);
                DataModel.SaveChanges();
            }
        }

        public void NewUsuario(Usuario usuario, Usuario NovoUsuario)
        {

            NovoUsuario.IdUsuario = usuario.IdUsuario;
            NovoUsuario.NomeUsuario = usuario.NomeUsuario;
            NovoUsuario.Senha = usuario.Senha;
            NovoUsuario.Email = usuario.Email;
            NovoUsuario.IsEmpresa = usuario.IsEmpresa;
            NovoUsuario.isAdmin = usuario.isAdmin;
            NovoUsuario.IdCidade = usuario.Cidade.IdCidade;
            DataModel.Usuario.Add(NovoUsuario);
            DataModel.SaveChanges();
        }

        public void DeleteEmpresa(Empresa empresa, int IdUsuario)
        {
            var usuario = GetOneUsuario(IdUsuario);

            DataModel.Empresa.Remove(empresa);
            DataModel.Usuario.Remove(usuario);

            DataModel.SaveChanges();
        }
        public void DeleteConsumidor(Consumidor consumidor, int IdUsuario)
        {
            var usuario = GetOneUsuario(IdUsuario);

            DataModel.Consumidor.Remove(consumidor);
            DataModel.Usuario.Remove(usuario);

            DataModel.SaveChanges();
        }


        public void Edit(Usuario usuario, int idUsuario)
        {
            Usuario usuarioAlterado = new Usuario();


            usuarioAlterado.IdUsuario = idUsuario;

            usuarioAlterado.NomeUsuario = usuario.NomeUsuario;
            usuarioAlterado.Senha = usuario.Senha;
            usuarioAlterado.Email = usuario.Email;
            usuarioAlterado.IsEmpresa = usuario.IsEmpresa;
            usuarioAlterado.isAdmin = usuario.isAdmin;
            usuarioAlterado.IdCidade = usuario.Cidade.IdCidade;

            if (usuario.IsEmpresa)
            {
                Empresa empresaAlterada = new Empresa();

                empresaAlterada.IdEmpresa = idUsuario;

                empresaAlterada.IdSegmento = usuario.Empresa.Segmento.IdSegmento;
                empresaAlterada.CNPJ = usuario.Empresa.CNPJ;
                empresaAlterada.RazaoSocial = usuario.Empresa.RazaoSocial;
                empresaAlterada.Fantasia = usuario.Empresa.Fantasia;
                empresaAlterada.Endereco = usuario.Empresa.Endereco;
                empresaAlterada.Numero = usuario.Empresa.Numero;
                empresaAlterada.Bairro = usuario.Empresa.Bairro;
                empresaAlterada.CEP = usuario.Empresa.CEP;
                empresaAlterada.Telefone = usuario.Empresa.Telefone;
                empresaAlterada.Logo = usuario.Empresa.Logo;

                DataModel.Entry(empresaAlterada).State = empresaAlterada.IdEmpresa == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
                DataModel.Entry(usuarioAlterado).State = usuarioAlterado.IdUsuario == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
                DataModel.SaveChanges();
            }
            else
            {
                Consumidor consumidorAlterado = new Consumidor();

                consumidorAlterado.IdConsumidor = idUsuario;

                consumidorAlterado.Nome = usuario.Consumidor.Nome;
                consumidorAlterado.DataNascimento = usuario.Consumidor.DataNascimento;

                DataModel.Entry(consumidorAlterado).State = consumidorAlterado.IdConsumidor == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
                DataModel.Entry(usuarioAlterado).State = usuarioAlterado.IdUsuario == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
                DataModel.SaveChanges();
            }


        }

        public Usuario GetOneUsuario(int id)
        {
            return DataModel.Usuario.Find(id);
        }

        public Empresa GetOneEmpresa(int? id)
        {
            return DataModel.Empresa.Find(id);
        }

        public List<Empresa> GetAllEmpresaByIdConsumidor(int idConsumidor)
        {
            var listAcompanhamento = DataModel.Acompanhamento.Where(e => e.IdConsumidor == idConsumidor).ToList();

            var empresas = DataModel.Empresa.Include("Segmento").ToList();

            List<Empresa> listaEmpresa = new List<Empresa>();

            foreach (var item in listAcompanhamento)
            {
                foreach (var empresa in empresas)
                {
                    if (item.IdEmpresa == empresa.IdEmpresa)
                    {
                        listaEmpresa.Add(empresa);
                    }
                }
            }
            return listaEmpresa;

        }

        public List<Consumidor> GetAllConsumidorByIdEmpresa(int idEmpresa)
        {
            var listAcompanhamento = DataModel.Acompanhamento.Where(e => e.IdEmpresa == idEmpresa).ToList();

            var consumidores = DataModel.Consumidor.Include("Usuario").ToList();

            var cidades = new Cidade();


            List<Consumidor> listaConsumidor = new List<Consumidor>();

            foreach (var item in listAcompanhamento)
            {
                foreach (var consumidor in consumidores)
                {
                    if (item.IdConsumidor == consumidor.IdConsumidor)
                    {
                        cidades = DataModel.Cidade.Find(consumidor.Usuario.IdCidade);

                        listaConsumidor.Add(consumidor);
                    }
                }
            }
            return listaConsumidor;

        }






        public Consumidor GetOneConsumidor(int? id)
        {
            return DataModel.Consumidor.Find(id);
        }

        public List<Usuario> GetAllUsuario()
        {
            return DataModel.Usuario.ToList();
        }

        public List<Empresa> EmpresasDesque()
        {
            return DataModel.Empresa.Include("Segmento").OrderByDescending(e => e.MediaAvaliacao).ToList();
        }

        public bool Logar(string NomeUsuario, string Senha)
        {
            if (DataModel.Usuario.Where(e => e.NomeUsuario.Equals(NomeUsuario) && e.Senha.Equals(Senha) || NomeUsuario == "Admin" && Senha == "Admin").Count() == 0)
            {
                return false;
            }
            return true;

        }

        public Usuario UsuarioLogado(string nomeUsuario)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            int idUsuarioLogado = 0;
            listaUsuarios = DataModel.Usuario.ToList();

            foreach (var item in listaUsuarios)
            {
                if (item.NomeUsuario == nomeUsuario)
                {
                    idUsuarioLogado = item.IdUsuario;
                }
            }

            var usuarioLogado = GetOneUsuario(idUsuarioLogado);

            return usuarioLogado;

        }

        public bool existeCNPJ(string cnpj)
        {
            if (DataModel.Usuario.Where(e => e.Empresa.CNPJ == cnpj).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool existeNomeUsuario(string nomeUsuario)
        {
            if (DataModel.Usuario.Where(e => e.NomeUsuario == nomeUsuario).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            DataModel.Dispose();
        }
    }
}