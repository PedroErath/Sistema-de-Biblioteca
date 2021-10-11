using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public int Inserir(Usuario user)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(user);
                bc.SaveChanges();
                return user.Id;
            }
        }
        public void Atualizar(Usuario user)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario registro = bc.Usuarios.Find(user.Id);
                if(registro != null){
                    registro.Nome = user.Nome;
                    registro.Login = user.Login;
                    registro.Senha = user.Senha;
                    registro.Tipo = user.Tipo;

                    bc.SaveChanges();
                }
            }
        }
        public void ExcluirUsuario(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {  
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
        public List<Usuario> ListarTodos()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
    }
}