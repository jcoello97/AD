package org.institutoserpis.ad;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;

@Entity(name = "Pedido")
public class Pedido {
	@Id
	@GeneratedValue
	private long id;
	@OneToMany(mappedBy = "pedido", cascade = CascadeType.ALL, orphanRemoval = true)
	private List<PedidoLinea> pedidoLineas = new ArrayList<>();

	// TODO Cambiar tipo en un futuro
	private String fecha;

	private BigDecimal importe;
	
	 
	@ManyToOne
	@JoinColumn (name = "cliente_id")
	private Cliente cliente;

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public List<PedidoLinea> getPedidoLineas() {
		return pedidoLineas;
	}

	public void addPedidoLinea(PedidoLinea pedidoLinea) {
		pedidoLineas.add(pedidoLinea);
		pedidoLinea.setPedido(this);
	}

	public void removePedidoLinea(PedidoLinea pedidoLinea) {
		pedidoLineas.remove(pedidoLinea);
		pedidoLinea.setPedido(null);
	}

	public String getFecha() {
		return fecha;
	}

	public void setFecha(String fecha) {
		this.fecha = fecha;
	}

	public BigDecimal getImporte() {
		return importe;
	}

	public void setImporte(BigDecimal importe) {
		this.importe = importe;
	}

	public Cliente getCliente() {
		return cliente;
	}

	public void setCliente(Cliente cliente) {
		this.cliente = cliente;
	}
}
