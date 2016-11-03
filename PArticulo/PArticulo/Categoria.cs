using System;

namespace PArticulo
{
	public class Categoria
	{
		public Categoria (long id, string nombre) {
			Id = id;
			Nombre = nombre;
		}

		public long Id { get; set; }
		public string Nombre { get; set; }

		public String toString(){
			return Nombre;
		}
	}
}

