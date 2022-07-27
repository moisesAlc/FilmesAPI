using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
	[ApiController]
	[Route("controller")]
	public class FilmeController : ControllerBase
	{
		private FilmeContext _context;

		public FilmeController(FilmeContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult AdicionaFilme([FromBody]CreateFilmeDTO filmeDTO)
		{
			Filme filme = new Filme()
			{
				Titulo = filmeDTO.Titulo,
				Diretor = filmeDTO.Diretor,
				Duracao = filmeDTO.Duracao,
				Genero = filmeDTO.Genero
			};

			_context.Filmes.Add(filme);
			_context.SaveChanges();
			return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filmeDTO);
		}

		[HttpGet]
		public IEnumerable<Filme> RecuperaFilmes()
		{
			return _context.Filmes;
		}

		[HttpGet("{id}")]
		public IActionResult RecuperaFilmesPorId(int id)
		{
			Filme filme =  _context.Filmes.FirstOrDefault(filme => filme.Id == id);
			if (filme != null)
			{
				return Ok(filme);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpPut("{id}")]
		public IActionResult AtualizaFilme(int id, [FromBody] Filme filmeNovo)
		{
			Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
			if (filme != null)
			{
				filme.Titulo = filmeNovo.Titulo;
				filme.Duracao = filmeNovo.Duracao;
				filme.Diretor = filmeNovo.Diretor;
				filme.Genero = filmeNovo.Genero;
				_context.SaveChanges();
				return NoContent();
			}
			else
			{
				return NotFound();
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeletaFilme(int id)
		{
			Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
			if (filme != null)
			{
				_context.Filmes.Remove(filme);
				_context.SaveChanges();
				return NoContent();
			}
			else
			{
				return NotFound();
			}
		}
	}
}