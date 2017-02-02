package org.institutoserpis.ad;

import java.math.BigDecimal;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

@Entity(name =  "PedidoLinea")
public class PedidoLinea {
	@Id
	@GeneratedValue
	private long id;
	
	@ManyToOne
	@JoinColumn (name = "pedido_id")
	private Pedido pedido;
	
	@ManyToOne
	@JoinColumn (name = "articulo_id")
	private Articulo articulo;
	
	private BigDecimal precio;
	
	private BigDecimal unidades;

	private BigDecimal importe;
	

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public Pedido getPedido() {
		return pedido;
	}

	public void setPedido(Pedido pedido) {
		this.pedido = pedido;
	}

	public Articulo getArticulo() {
		return articulo;
	}

	public void setArticulo(Articulo articulo) {
		this.articulo = articulo;
	}

	public BigDecimal getPrecio() {
		return precio;
	}

	public void setPrecio(BigDecimal precio) {
		this.precio = precio;
	}

	public BigDecimal getUnidades() {
		return unidades;
	}

	public void setUnidades(BigDecimal unidades) {
		this.unidades = unidades;
	}

	public BigDecimal getImporte() {
		return importe;
	}

	public void setImporte(BigDecimal importe) {
		this.importe = importe;
	}
	
	
	
}
