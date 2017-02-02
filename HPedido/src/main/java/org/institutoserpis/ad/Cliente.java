package org.institutoserpis.ad;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity(name="Cliente")
public class Cliente {
	@Id
	@GeneratedValue
	private long id;
	
	private String nombre;
	/*
	@OneToMany (mappedBy = "cliente", cascade = CascadeType.ALL, orphanRemoval = true)
	private List<Pedido> pedidos = new ArrayList<>();*/
	
	public long getId() {
		return id;
	}
	public void setId(long id) {
		this.id = id;
	}
	public String getNombre() {
		return nombre;
	}
	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	/*
	public void addPedido (Pedido pedido){
		pedidos.add(pedido);
		pedido.setCliente(this);
	}
	public void removePedido(Pedido pedido){
		pedidos.remove(pedido);
		pedido.setCliente(null);
	}*/
}
