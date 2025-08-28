<!DOCTYPE html>
<html lang="pt-BR">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Carrinho do Cliente</title>
<script src="https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js"></script>
<link href="https://fonts.googleapis.com/css2?family=Quicksand:wght@400;600&display=swap" rel="stylesheet">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
<style>
  body{font-family:'Quicksand',sans-serif;margin:0;padding:20px;background:#E6F2EF;} 
  .container{max-width:900px;margin:auto;}
  h1{text-align:center;color:#32174D;margin-bottom:20px;}
  .logout{position:fixed;top:20px;left:20px;background:#f44336;color:#fff;padding:8px 12px;border:none;border-radius:6px;cursor:pointer;}
  .logout:hover{background:#d32f2f;}
  .carrinho{display:flex;flex-direction:column;gap:15px;}
  .item{background:#fff;padding:15px;border-radius:10px;display:flex;align-items:center;justify-content:space-between;box-shadow:0 2px 6px rgba(0,0,0,0.05);}
  .item img{width:80px;height:80px;object-fit:cover;border-radius:6px;margin-right:10px;}
  .item-info{display:flex;align-items:center;gap:10px;flex:1;}
  .acoes button{padding:6px 10px;border:none;background:#32174D;color:white;border-radius:6px;cursor:pointer;}
  .acoes button:hover{background:#3EB489; color:white;}
  .btn-pagar{margin-top:20px;background:#3EB489;color:white;padding:10px 16px;border:none;border-radius:8px;cursor:pointer;display:block;margin:auto;font-size:16px;}
  .btn-pagar:hover{background:#32174D; color:white;}
  a{display:block;text-align:center;margin-top:20px;color:#32174D;text-decoration:none;}
  .resumo{margin-top:15px;padding:15px;background:#fff;border-radius:10px;box-shadow:0 2px 6px rgba(0,0,0,0.05);text-align:right;font-size:18px;font-weight:600;color:#32174D;}
  .modal{position:fixed;top:0;left:0;width:100%;height:100%;background:rgba(0,0,0,0.5);display:flex;align-items:center;justify-content:center;z-index:100;}
  .modal-conteudo{background:#fff;padding:20px;border-radius:10px;text-align:center;max-width:400px;width:90%;}
  .modal-conteudo h2{margin-bottom:10px;color:#32174D;}
  .modal-conteudo p.total{font-weight:bold;color:#3EB489;margin-bottom:15px;}
  .opcoes button{margin:8px;padding:10px 16px;border:none;border-radius:8px;cursor:pointer;}
  .pix{background:#3EB489;color:#fff;}
  .cartao{background:#32174D;color:#fff;}
  .dinheiro{background:#f4b400;color:#fff;}
  .fechar{margin-top:15px;background:#ccc;color:#000;}
  .qr-code{margin:20px 0;}
  .qr-code img{width:200px;height:200px;}
  .confirmar{margin-top:15px;background:#3EB489;color:#fff;padding:10px 16px;border:none;border-radius:8px;cursor:pointer;}
  .form-cartao{display:flex;flex-direction:column;gap:10px;text-align:left;}
  .form-cartao label{font-size:14px;font-weight:600;color:#32174D;}
  .form-cartao input,.form-cartao select{padding:8px;border:1px solid #ccc;border-radius:6px;}
</style>
</head>
<body>
<div id="app" class="container">
  <button class="logout" @click="sair">Sair</button>
  <h1><i class="fas fa-shopping-cart"></i> Carrinho</h1>

  <div class="carrinho" v-if="carrinhoAgrupado.length">
    <div class="item" v-for="(item,index) in carrinhoAgrupado" :key="item.id">
      <div class="item-info">
        <img :src="item.imagem">
        <div>
          <p><strong>{{ item.nome }}</strong> <span v-if="item.quantidade>1">x {{ item.quantidade }}</span></p>
          <p>{{ formatarMoeda(item.valor * item.quantidade) }}</p>
        </div>
      </div>
      <div class="acoes">
        <button @click="removerItem(item.id)">Excluir</button>
      </div>
    </div>

    <div class="resumo">Total: {{ formatarMoeda(total) }}</div>
    <button class="btn-pagar" @click="abrirPagamento">💳 Pagar</button>
  </div>

  <p v-else>O carrinho está vazio.</p>
  <a href="index.html"><i class="fas fa-arrow-left"></i> Voltar para Produtos</a>

  <!-- Modal de Pagamento -->
  <div class="modal" v-if="mostrarModal">
    <div class="modal-conteudo">
      <h2>Escolha a forma de pagamento</h2>
      <p class="total">Total: {{ formatarMoeda(total) }}</p>

      <div class="opcoes" v-if="!pagamentoSelecionado">
        <button class="pix" @click="pagamentoSelecionado='pix'"><i class="fas fa-qrcode"></i> Pix</button>
        <button class="cartao" @click="pagamentoSelecionado='cartao'"><i class="fas fa-credit-card"></i> Cartão</button>
        <button class="dinheiro" @click="pagamentoSelecionado='dinheiro'"><i class="fas fa-money-bill-wave"></i> Dinheiro</button>
      </div>

      <!-- PIX -->
      <div v-if="pagamentoSelecionado==='pix'">
        <h3>Pagamento via Pix</h3>
        <div class="qr-code">
          <img :src="'https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=PagamentoPix' + total" alt="QR Code Pix">
        </div>
        <p>Escaneie o QR Code com seu banco para concluir o pagamento.</p>
        <button class="confirmar" @click="confirmarPagamento">Confirmar</button>
      </div>

      <!-- CARTÃO -->
      <div v-if="pagamentoSelecionado==='cartao'" class="form-cartao">
        <h3>Pagamento com Cartão</h3>

        <label>Número do Cartão</label>
        <input type="text" v-model="cartao.numero" placeholder="0000 0000 0000 0000">

        <label>Validade</label>
        <input type="text" v-model="cartao.validade" placeholder="MM/AA">

        <label>CVV</label>
        <input type="text" v-model="cartao.cvv" placeholder="123">

        <label>Parcelamento</label>
        <select v-model="cartao.parcelas">
          <option v-for="n in 12" :value="n">
            {{ n }}x de {{ formatarMoeda(total/n) }}
          </option>
        </select>

        <p style="margin-top:10px;font-weight:bold;color:#3EB489;">
          Total: {{ formatarMoeda(total) }} 
          <span v-if="cartao.parcelas"> ({{ cartao.parcelas }}x de {{ formatarMoeda(total/cartao.parcelas) }})</span>
        </p>

        <button class="confirmar" @click="confirmarPagamento">Pagar</button>
      </div>

      <!-- DINHEIRO -->
      <div v-if="pagamentoSelecionado==='dinheiro'">
        <h3>Pagamento em Dinheiro</h3>
        <p>O pagamento será realizado na entrega/caixa.</p>
        <button class="confirmar" @click="confirmarPagamento">Confirmar</button>
      </div>

      <button class="fechar" @click="fecharModal">Fechar</button>
    </div>
  </div>
</div>

<script>
new Vue({
  el:'#app',
  data:{ 
    carrinho:[],
    mostrarModal:false,
    pagamentoSelecionado:null,
    cartao:{numero:'', validade:'', cvv:'', parcelas:1}
  },
  computed:{
    carrinhoAgrupado(){
      const map = {};
      this.carrinho.forEach(prod=>{
        if(map[prod.id]){ map[prod.id].quantidade++; }
        else{ map[prod.id] = {...prod, quantidade:1}; }
      });
      return Object.values(map);
    },
    total(){ return this.carrinho.reduce((soma,prod)=>soma+prod.valor,0); }
  },
  created(){
    // Verifica login
    const logado = localStorage.getItem("logado");
    if(!logado){ window.location.href = "login.html"; }
    this.carregar();
  },
  methods:{
    carregar(){ 
      const dados=localStorage.getItem('carrinho'); 
      if(dados)this.carrinho=JSON.parse(dados); 
    },
    removerItem(id){
      this.carrinho = this.carrinho.filter(prod => prod.id !== id);
      localStorage.setItem('carrinho',JSON.stringify(this.carrinho));
    },
    formatarMoeda(valor){ return new Intl.NumberFormat('pt-BR',{style:'currency',currency:'BRL'}).format(valor); },
    abrirPagamento(){
      if(this.carrinho.length===0){ alert('O carrinho está vazio!'); return; }
      this.mostrarModal = true;
      this.pagamentoSelecionado = null;
    },
    fecharModal(){
      this.mostrarModal = false;
      this.pagamentoSelecionado = null;
    },
    confirmarPagamento(){
      alert("Pagamento confirmado! Obrigado pela compra.");
      this.carrinho = [];
      localStorage.removeItem("carrinho");
      this.fecharModal();
    },
    sair(){
      localStorage.removeItem("logado");
      window.location.href = "login.html";
    }
  }
});
</script>
</body>
</html>
