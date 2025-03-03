import React, { useState } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity, FlatList } from "react-native";
import { doc, setDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";

const DadosClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const user = auth.currentUser;
  if (!user) {
    Alert.alert("Erro", "Nenhuma clinica autenticada!");
    navigation.goBack();
    return null;
  }

  const [step, setStep] = useState(1);
  
  const [nome, setNome] = useState("");
  const [cnpj, setCnpj] = useState("");

  const [cep, setCep] = useState("");
  const [estado, setEstado] = useState("");
  const [bairro, setBairro] = useState("");
  const [cidade, setCidade] = useState("");
  const [rua, setRua] = useState("");
  const [numero, setNumero] = useState("");

  const [especialidades, setEspecialidades] = useState<string[]>([]);

  // Lista de especialidades disponíveis neste momento
  const listaEspecialidades = [
    "Ortodontia",
    "Implantes Dentários",
    "Clínica Geral",
    "Prótese Dentária",
    "Odontologia Estética",
    "Cirurgia Bucal",
  ];

  const [diasSemana, setDiasSemana] = useState<string[]>([]);
  
  // Lista com os dias disponíveis neste momento
  const listaDiasSemana = [
    "Segunda",
    "Terca",
    "Quarta",
    "Quinta",
    "Sexta",
    "Sábado",
  ];

  const [turno, setTurno] = useState<string[]>([]);

  // Lista com os turnos disponíveis neste momento
  const listaTurnos = [
    "Manhã",
    "Tarde",
    "Noite",
  ];

  const [mensagem, setMensagem] = useState("");

  const handleSelectEspecialidade = (especialidade: string) => {
    setEspecialidades(prevState => {
      if (prevState.includes(especialidade)) {
        return prevState.filter(item => item !== especialidade);
      } else {
        return [...prevState, especialidade];
      }
    });
  };

  const handleSelectDias = (diasSemana: string) => {
    setDiasSemana(prevState => {
      if (prevState.includes(diasSemana)) {
        return prevState.filter(item => item !== diasSemana);
      } else {
        return [...prevState, diasSemana];
      }
    });
  };

  const handleSelectTurnos = (turno: string) => {
    setTurno(prevState => {
      if (prevState.includes(turno)) {
        return prevState.filter(item => item !== turno);
      } else {
        return [...prevState, turno];
      }
    });
  };


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
          <Text style={styles.sectionTitle}>Dados Cadastrais</Text>
          <TextInput style={styles.input} placeholder="Nome" value={nome} onChangeText={setNome} />
          <TextInput style={styles.input} placeholder="CNPJ" value={cnpj} onChangeText={setCnpj} />
    
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_dados_cadastrais_clinicas", { nome, cnpj})}>
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
          <Text style={styles.sectionTitle}>Endereço</Text>
          <TextInput style={styles.input} placeholder="CEP" value={cep} onChangeText={setCep} />
          <TextInput style={styles.input} placeholder="Estado" value={estado} onChangeText={setEstado} />
          <TextInput style={styles.input} placeholder="Bairro" value={bairro} onChangeText={setBairro} />
          <TextInput style={styles.input} placeholder="Cidade" value={cidade} onChangeText={setCidade} />
          <TextInput style={styles.input} placeholder="Rua" value={rua} onChangeText={setRua} />
          <TextInput style={styles.input} placeholder="Número" value={numero} onChangeText={setNumero} />
          
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_endereco_clinica", { cep, estado, cidade,bairro, rua, numero })}>
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
            <Text style={styles.sectionTitle}>Especialidades</Text>

          <FlatList
            data={listaEspecialidades}
            keyExtractor={(item) => item}
            renderItem={({ item }) => (
              <TouchableOpacity
                style={[
                  styles.specialityItem,
                  especialidades.includes(item) && styles.selectedSpeciality,
                ]}
                onPress={() => handleSelectEspecialidade(item)}
              >
                <Text style={styles.specialityText}>{item}</Text>
              </TouchableOpacity>
            )}
          />

          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_especialidades", { especialidades})}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>
          
          
          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
          
          <TouchableOpacity onPress={() => setStep(1)} style={styles.prevButton}>
            <Text style={styles.buttonText}>← Voltar</Text>
          </TouchableOpacity>
          
          <TouchableOpacity onPress={() => setStep(4)} style={styles.nextButton}>
            <Text style={styles.buttonText}>→ Próximo</Text>
          </TouchableOpacity>
          </View>
      )}

      {step === 4 && (
        <View style={styles.section}>
        <Text style={styles.sectionTitle}>Dias disponíveis</Text>

      <FlatList
        data={listaDiasSemana}
        keyExtractor={(item) => item}
        renderItem={({ item }) => (
          <TouchableOpacity
            style={[
              styles.specialityItem,
              diasSemana.includes(item) && styles.selectedSpeciality,
            ]}
            onPress={() => handleSelectDias(item)}
          >
            <Text style={styles.specialityText}>{item}</Text>
          </TouchableOpacity>
        )}
      />

      <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_dias_preferencia_clinicas", { diasSemana})}>
        <Text style={styles.buttonText}>Salvar</Text>
      </TouchableOpacity>
      
      
      {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
      
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
        <Text style={styles.sectionTitle}>Turnos</Text>

      <FlatList
        data={listaTurnos}
        keyExtractor={(item) => item}
        renderItem={({ item }) => (
          <TouchableOpacity
            style={[
              styles.specialityItem,
              turno.includes(item) && styles.selectedSpeciality,
            ]}
            onPress={() => handleSelectTurnos(item)}
          >
            <Text style={styles.specialityText}>{item}</Text>
          </TouchableOpacity>
        )}
      />

      <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_turno_preferencia_clinicas", { turno})}>
        <Text style={styles.buttonText}>Salvar</Text>
      </TouchableOpacity>
      
      
      {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
      
      <TouchableOpacity onPress={() => setStep(4)} style={styles.prevButton}>
        <Text style={styles.buttonText}>← Voltar</Text>
      </TouchableOpacity>

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
  sectionTitle: { 
    fontSize: 18, 
    fontWeight: "bold", 
    marginBottom: 10 },
  input: { 
    width: "100%", 
    height: 40, 
    borderBottomWidth: 1, 
    borderColor: "#ccc", 
    marginBottom: 10 },
  mensagem: { 
    marginTop: 10, 
    fontSize: 14, 
    color: "green" },
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
  specialityItem: {
    padding: 15,
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 8,
    marginBottom: 10,
  },
  selectedSpeciality: {
    backgroundColor: "#08c8f8",
  },
  specialityText: {
    fontSize: 16,
    color: "#081828",
  },
});

export default DadosClinicaScreen;
