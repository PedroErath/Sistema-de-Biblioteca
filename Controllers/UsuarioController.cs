using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().ListarTodos());
        }
        public IActionResult editarUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Usuario u = new UsuarioService().Listar(id);

            return View(u);
        }
        [HttpPost]
        public IActionResult editarUsuario(Usuario user)
        {
            UsuarioService us = new UsuarioService();
            us.Atualizar(user);

            return RedirectToAction("ListaDeUsuarios");
        }
        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario user)
        {
            user.Senha = Criptografo.TextoCriptografado(user.Senha);

            UsuarioService us = new UsuarioService();
            us.Inserir(user);

            return RedirectToAction("CadastroRealizado");
        }
        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View(new UsuarioService().Listar(id));
        }
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if(decisao=="EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuário "+ new UsuarioService().Listar(id).Nome +" realizada com sucesso";
                UsuarioService us = new UsuarioService();
                us.ExcluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().ListarTodos());
            }
            else
            {
                ViewData["Mnesagem"] ="Exclusão cancelada";
                return View("ListaDeUsuarios",new UsuarioService().ListarTodos());
            }
        }
        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}