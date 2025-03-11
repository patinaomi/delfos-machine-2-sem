import React, { useState } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity } from "react-native";
import { doc, setDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";
import Footer from "../components/Footer";

const CadastroPessoalClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const user = auth.currentUser;
  if (!user) {
    Alert.alert("Erro", "Nenhum usuário autenticado!");
    navigation.goBack();
    return null;
  }

  const [step, setStep] = useState(1);
  
  const [nome, setNome] = useState("");
  const [sobrenome, setSobrenome] = useState("");
  const [cpf, setCpf] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [idade, setIdade] = useState("");
  const [altura, setAltura] = useState("");

  const [cepResidencia, setCepResidencia] = useState("");
  const [estadoResidencia, setEstadoResidencia] = useState("");
  const [cidadeResidencia, setCidadeResidencia] = useState("");
  const [bairroResidencia, setBairroResidencia] = useState("");
  const [ruaResidencia, setRuaResidencia] = useState("");
  const [numeroResidencia, setNumeroResidencia] = useState("");

  const [cepConsulta, setCepConsulta] = useState("");
  const [estadoConsulta, setEstadoConsulta] = useState("");
  const [cidadeConsulta, setCidadeConsulta] = useState("");
  const [bairroConsulta, setBairroConsulta] = useState("");
  const [ruaConsulta, setRuaConsulta] = useState("");
  const [numeroConsulta, setNumeroConsulta] = useState("");

  const [diasSemana, setDiasSemana] = useState<string[]>([]);
  const [turno, setTurno] = useState("");

  const [mensagem, setMensagem] = useState("");

  const salvarDados = async (colecao: string, dados: object) => {
    try {
      await setDoc(doc(db, colecao, user.uid), dados, { merge: true });
      setMensagem("✅ Dados salvos com sucesso!");
    } catch (error: any) {
      setMensagem("❌ Erro ao salvar os dados.");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Delfos Machine</Text>
      <View style={styles.tittleLine} />

      {step === 1 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Dados Pessoais</Text>
          <TextInput style={styles.input} placeholder="Nome" value={nome} onChangeText={setNome} />
          <TextInput style={styles.input} placeholder="Sobrenome" value={sobrenome} onChangeText={setSobrenome} />
          <TextInput style={styles.input} placeholder="CPF" value={cpf} onChangeText={setCpf} />
          <TextInput style={styles.input} placeholder="Data de Nascimento" value={dataNascimento} onChangeText={setDataNascimento} />
          <TextInput style={styles.input} placeholder="Idade" keyboardType="numeric" value={idade} onChangeText={setIdade} />
          <TextInput style={styles.input} placeholder="Altura (m)" keyboardType="decimal-pad" value={altura} onChangeText={setAltura} />
    
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_dados_cadastrais", { nome, sobrenome, cpf, dataNascimento, idade, altura })}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>

          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
          <TouchableOpacity onPress={() => setStep(2)} style={styles.nextButton}>
            <Text style={styles.buttonText}>→ Próximo</Text>
          </TouchableOpacity>
        </View>
      )}

      {step === 2 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Endereço Residência</Text>
          <TextInput style={styles.input} placeholder="CEP" value={cepResidencia} onChangeText={setCepResidencia} />
          <TextInput style={styles.input} placeholder="Estado" value={estadoResidencia} onChangeText={setEstadoResidencia} />
          <TextInput style={styles.input} placeholder="Cidade" value={cidadeResidencia} onChangeText={setCidadeResidencia} />
          <TextInput style={styles.input} placeholder="Bairro" value={bairroResidencia} onChangeText={setBairroResidencia} />
          <TextInput style={styles.input} placeholder="Rua" value={ruaResidencia} onChangeText={setRuaResidencia} />
          <TextInput style={styles.input} placeholder="Número" value={numeroResidencia} onChangeText={setNumeroResidencia} />
          
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_endereco_residencia", { cepResidencia, estadoResidencia, cidadeResidencia, bairroResidencia, ruaResidencia, numeroResidencia })}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>

          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
          <TouchableOpacity onPress={() => setStep(1)} style={styles.prevButton}>
            <Text style={styles.buttonText}>← Voltar</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => setStep(3)} style={styles.nextButton}>
            <Text style={styles.buttonText}>→ Próximo</Text>
          </TouchableOpacity>
        </View>
      )}

    {step === 3 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Endereço Preferência</Text>

          <TextInput style={styles.input} placeholder="CEP" value={cepConsulta} onChangeText={setCepResidencia} />
          <TextInput style={styles.input} placeholder="Estado" value={estadoConsulta} onChangeText={setEstadoResidencia} />
          <TextInput style={styles.input} placeholder="Cidade" value={cidadeConsulta} onChangeText={setCidadeResidencia} />
          <TextInput style={styles.input} placeholder="Bairro" value={bairroConsulta} onChangeText={setBairroResidencia} />
          <TextInput style={styles.input} placeholder="Rua" value={ruaConsulta} onChangeText={setRuaResidencia} />
          <TextInput style={styles.input} placeholder="Número" value={numeroConsulta} onChangeText={setNumeroResidencia} />
 
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_endereco_preferencia", { cepConsulta, estadoConsulta, cidadeConsulta,bairroConsulta, ruaConsulta, numeroConsulta })}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>
          
          
          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
          
          <TouchableOpacity onPress={() => setStep(2)} style={styles.prevButton}>
            <Text style={styles.buttonText}>← Voltar</Text>
          </TouchableOpacity>
          
          <TouchableOpacity onPress={() => setStep(4)} style={styles.nextButton}>
            <Text style={styles.buttonText}>→ Próximo</Text>
          </TouchableOpacity>
        </View>
      )}

      {step === 4 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Preferências</Text>
          <TextInput style={styles.input} placeholder="Dias da Semana (ex: Segunda, Terça)" value={diasSemana.join(", ")} onChangeText={(text) => setDiasSemana(text.split(", "))} />
          
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_dias_preferencia", { diasSemana })}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>
          
          <TouchableOpacity onPress={() => setStep(3)} style={styles.prevButton}>
            <Text style={styles.buttonText}>← Voltar</Text>
          </TouchableOpacity>

          <TouchableOpacity onPress={() => setStep(5)} style={styles.nextButton}>
            <Text style={styles.buttonText}>→ Próximo</Text>
          </TouchableOpacity>

        </View>
      )}

{step === 5 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Preferências</Text>
          <TextInput style={styles.input} placeholder="Turno (Manhã, Tarde, Noite)" value={turno} onChangeText={setTurno} />
          
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_turno_preferencia", { turno})}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>
          
          <TouchableOpacity onPress={() => setStep(4)} style={styles.prevButton}>
            <Text style={styles.buttonText}>← Voltar</Text>
          </TouchableOpacity>
          
          <Footer textColor='#000'/>
        </View>
      )}
      
    </View>
  );
};

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    padding: 20, 
    backgroundColor: "#f9f9f9", 
    justifyContent: "center",
    width: '100%'
  },
  title: { 
    fontSize: 24, 
    fontWeight: "bold", 
    textAlign: "center", 
    marginBottom: 20 ,
  },
  tittleLine: {
    width: 100,
    height: 2,
    backgroundColor: "#ccc",
    marginTop: 5,
  },
  section: { 
    backgroundColor: "#fff", 
    padding: 15, 
    borderRadius: 10, 
    alignItems: "center",
    width: '100%',
  },
  sectionTitle: { fontSize: 18, fontWeight: "bold", marginBottom: 10 },
  input: { width: "100%", height: 40, borderBottomWidth: 1, borderColor: "#ccc", marginBottom: 10 },
  mensagem: { marginTop: 10, fontSize: 14, color: "green" },
  nextButton: { 
    marginTop: 10, 
    padding: 10, 
    backgroundColor: "#2196F3", 
    borderRadius: 10,
    width: '90%',
    alignItems: 'center'
  },
  prevButton: { 
    marginTop: 10, 
    padding: 10, 
    backgroundColor: "#D32F2F", 
    borderRadius: 10,
    width: '90%',
    alignItems: 'center'
  },
  buttonText: { 
    fontSize: 16, 
    color: "#fff", 
    fontWeight: "bold" },
  buttonSave: {
    width: "90%",
    backgroundColor: "#08c8f8",
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 10,
    fontWeight: "bold",
    color: '#fff'
  },
});

export default CadastroPessoalClienteScreen;
