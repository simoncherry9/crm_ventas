using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.BLL.Implementacion
{
	public class NegocioService : INegocioService
	{
		private readonly IGenericRepository<Negocio> _repositorio;
		private readonly IFireBaseService _firebaseService;

		public NegocioService(IGenericRepository<Negocio> repositorio, IFireBaseService firebaseService)
		{
			_repositorio = repositorio;
			_firebaseService = firebaseService;
		}

		public async Task<Negocio> Obtener()
		{
			try
			{
				Negocio negocio_encontrado = await _repositorio.Obtener(n => n.IdNegocio == 1);
				return negocio_encontrado;
			}
			catch
			{
				throw;
			}
		}
		public async Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null, string NombreLogo = "")
		{
			try
			{
				Negocio negocio_encotrado = await _repositorio.Obtener(n => n.IdNegocio == 1);

				negocio_encotrado.NumeroDocumento = entidad.NumeroDocumento;
				negocio_encotrado.Nombre = entidad.Nombre;
				negocio_encotrado.Correo = entidad.Correo;
				negocio_encotrado.Direccion = entidad.Direccion;
				negocio_encotrado.Telefono = entidad.Telefono;
				negocio_encotrado.PorcentajeImpuesto = entidad.PorcentajeImpuesto;
				negocio_encotrado.SimboloMoneda = entidad.SimboloMoneda;

				negocio_encotrado.NombreLogo = negocio_encotrado.NombreLogo == "" ? NombreLogo : negocio_encotrado.NombreLogo;

				if (Logo != null)
				{
					string urlLogo = await _firebaseService.SubirStorage(Logo, "carpeta_logo", negocio_encotrado.NombreLogo);
					negocio_encotrado.UrlLogo = urlLogo;
				}

				await _repositorio.Editar(negocio_encotrado);
				return negocio_encotrado;

			}
			catch
			{
				throw;
			}
		}
	}
}
