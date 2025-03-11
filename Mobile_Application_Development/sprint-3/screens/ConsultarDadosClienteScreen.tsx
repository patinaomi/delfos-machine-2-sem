import React, { useState, useEffect } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity, ScrollView, FlatList } from "react-native";
import { doc, getDoc, updateDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { getAuth } from "firebase/auth";

const ConsultarDadosClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const user = auth.currentUser;
  if (!user) {
    Alert.alert("Erro", "Nenhum usuário autenticado!");
    navigation.goBack();
    return null;
  }

  const [step, setStep] = useState(1);
  const [dados, setDados] = useState<any>({});
  const [mensagem, setMensagem] = useState("");
  const [loading, setLoading] = useState(true);
  const [enderecoResidencia, setEnderecoResidencia] = useState<any>({});
  const [enderecoConsulta, setEnderecoConsulta] = useState<any>({});

  const [diaPreferenciaCliente, setDiaPreferenciaCliente] = useState<string[]>([]);
  // Lista com os dias disponíveis neste momento
  const listaDiasSemana = [
    "Segunda",
    "Terca",
    "Quarta",
    "Quinta",
    "Sexta",
    "Sábado",
  ];

  const [turnoPreferenciaCliente, setTurnoPreferenciaCliente] = useState<string[]>([]);
  // Lista com os turnos disponíveis neste momento
  const listaTurnos = [
    "Manhã",
    "Tarde",
    "Noite",
  ];

  const handleSelectDiaPreferenciaCliente = (diaPreferenciaCliente: string) => {
    setDiaPreferenciaCliente(prevState => {
      if (prevState.includes(diaPreferenciaCliente)) {
        return prevState.filter(item => item !== diaPreferenciaCliente);
      } else {
        return [...prevState, diaPreferenciaCliente];
      }
    });
  };

  const handleSelectTurnoPreferenciaCliente = (turnoPreferenciaCliente: string) => {
    setTurnoPreferenciaCliente(prevState => {
      if (prevState.includes(turnoPreferenciaCliente)) {
        return prevState.filter(item => item !== turnoPreferenciaCliente);
      } else {
        return [...prevState, turnoPreferenciaCliente];
      }
    });
  };

  useEffect(() => {
    const buscarDados = async () => {
      try {
        const dadosCadastraisRef = doc(db, "t_dados_pessoais_clientes", user.uid);
        const enderecoResidenciaRef = doc(db, "t_endereco_residencia_cliente", user.uid);
        const enderecoConsultaRef = doc(db, "t_endereco_preferencia_cliente", user.uid);
        const diasPreferenciaRef = doc(db, "t_dia_preferencia_cliente", user.uid);
        const turnoPreferenciaRef = doc(db, "t_turno_preferencia_cliente", user.uid);
  
        const dadosCadastraisSnap = await getDoc(dadosCadastraisRef);
        const enderecoResidenciaSnap = await getDoc(enderecoResidenciaRef);
        const enderecoConsultaSnap = await getDoc(enderecoConsultaRef);
        const diasPreferenciaSnap = await getDoc(diasPreferenciaRef);
        const turnoPreferenciaSnap = await getDoc(turnoPreferenciaRef);
  
        setDados((prevDados: any) => ({
          ...prevDados, // Mantém os dados existentes
          ...(dadosCadastraisSnap.exists() ? dadosCadastraisSnap.data() : {}),
          ...(enderecoResidenciaSnap.exists() ? enderecoResidenciaSnap.data() : {}),
          ...(enderecoConsultaSnap.exists() ? enderecoConsultaSnap.data() : {}),
          ...(diasPreferenciaSnap.exists() ? diasPreferenciaSnap.data() : {}),
          ...(turnoPreferenciaSnap.exists() ? turnoPreferenciaSnap.data() : {}),
          idCliente: user.uid,
        }));

        // Carregar dias da semana
        if (diasPreferenciaSnap.exists()) {
          setDiaPreferenciaCliente(diasPreferenciaSnap.data().diaPreferenciaCliente || []);
        }

        // Carregar todas as especialidades disponíveis
        const diaSemanaDisponiveisRef = doc(db, "t_dia_preferencia_cliente", "todos");
        const diaSemanaDisponiveisSnap = await getDoc(diaSemanaDisponiveisRef);

        // Carregar turnos
        if (turnoPreferenciaSnap.exists()) {
          setTurnoPreferenciaCliente(turnoPreferenciaSnap.data().turnoPreferenciaCliente || []);
        }
        
        // Carregar todos os turnos
        const turnoDisponiveisRef = doc(db, "t_turno_preferencia_cliente", "todos");
        const turnoDisponiveisSnap = await getDoc(turnoDisponiveisRef);
  
      } catch (error) {
        console.error("Erro ao buscar dados:", error);
        Alert.alert("Erro", "Não foi possível buscar os dados.");
      } finally {
        setLoading(false);
      }
    };
  
    buscarDados();
  }, []);
  


  const atualizarDados = async (colecao: string, novosDados: object) => {
    try {

      const auth = getAuth();
      const IdCliente = auth.currentUser?.uid;

      if (!IdCliente) {
        Alert.alert("Erro", "ID do cliente não encontrado.");
        return;
      }
      
      const dadosComIdCliente = { ...novosDados, idCliente: IdCliente };
      const docRef = doc(db, colecao, IdCliente);
      
      //await updateDoc(docRef, novosDados);
      await updateDoc(docRef, dadosComIdCliente);
      setMensagem("✅ Dados atualizados com sucesso!");
    } catch (error) {
      console.error("Erro ao atualizar dados:", error);
      setMensagem("❌ Erro ao atualizar os dados.");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Meus Dados</Text>

      {loading ? (
        <Text>Carregando...</Text>
      ) : (
        <>
          {step === 1 && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>Dados pessoais</Text>
              <TextInput style={styles.input} placeholder="Nome" value={dados.nome || ""} onChangeText={(text) => setDados({ ...dados, nome: text })} />
              <TextInput style={styles.input} placeholder="Sobrenome" value={dados.sobrenome || ""} onChangeText={(text) => setDados({ ...dados, sobrenome: text })} />
              <TextInput style={styles.input} placeholder="CPF" value={dados.cpf || ""} onChangeText={(text) => setDados({ ...dados, cpf: text })}/>
              <TextInput style={styles.input} placeholder="Data de Nascimento" value={dados.dataNascimento || ""} onChangeText={(text) => setDados({ ...dados, dataNascimento: text })} />
              <TextInput style={styles.input} placeholder="Idade" keyboardType="numeric" value={dados.idade || ""} onChangeText={(text) => setDados({ ...dados, idade: text })} />
              <TextInput style={styles.input} placeholder="Altura (m)" keyboardType="decimal-pad" value={dados.altura || ""} onChangeText={(text) => setDados({ ...dados, altura: text })} />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_dados_pessoais_clientes", dados)} width={'100%'}/>
              
              <TouchableOpacity onPress={() => setStep(2)} style={styles.nextButton}>
                <Text style={styles.buttonText}>→ Próximo</Text>
              </TouchableOpacity>

            </View>
          )}

          {step === 2 && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>Endereço de residência</Text>
              
              <TextInput style={styles.input} placeholder="CEP" 
              value={dados.cepResidencia || ""} 
              onChangeText={(text) => setDados({ ...dados, cepResidencia: text })} />
              
              <TextInput style={styles.input} placeholder="Estado" 
              value={dados.estadoResidencia || ""} 
              onChangeText={(text) => setDados({ ...dados, estadoResidencia: text })} />
              
              <TextInput style={styles.input} placeholder="Cidade" 
              value={dados.cidadeResidencia || ""} 
              onChangeText={(text) => setDados({ ...dados, cidadeResidencia: text })} />
              
              <TextInput style={styles.input} placeholder="Rua" 
              value={dados.ruaResidencia || ""} 
              onChangeText={(text) => setDados({ ...dados, ruaResidencia: text })} />
              
              <TextInput style={styles.input} placeholder="Número" 
              value={dados.numeroResidencia || ""} 
              onChangeText={(text) => setDados({ ...dados, numeroResidencia: text })} />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_endereco_residencia_cliente", dados)} width={'100%'}/>
              
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
              <Text style={styles.sectionTitle}>Endereço de preferência para consulta</Text>
              
              <TextInput style={styles.input} placeholder="CEP" value={dados.cepPreferenciaCliente || ""} onChangeText={(text) => setDados({ ...dados, cepPreferenciaCliente: text })} />
              
              <TextInput style={styles.input} placeholder="Estado" 
              value={dados.estadoPreferenciaCliente || ""} 
              onChangeText={(text) => setDados({ ...dados, estadoPreferenciaCliente: text })} />
              
              <TextInput style={styles.input} placeholder="Cidade" 
              value={dados.cidadePreferenciaCliente || ""} onChangeText={(text) => setDados({ ...dados, cidadePreferenciaCliente: text })} />
              
              <TextInput style={styles.input} placeholder="Rua" 
              value={dados.ruaPreferenciaCliente || ""} 
              onChangeText={(text) => setDados({ ...dados, ruaPreferenciaCliente: text })} />
              
              <TextInput style={styles.input} placeholder="Número" 
              value={dados.numeroPreferenciaCliente || ""} 
              onChangeText={(text) => setDados({ ...dados, numeroPreferenciaCliente: text })} />
              
              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_endereco_preferencia_cliente", dados)} width={'100%'}/>
              
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
              <Text style={styles.sectionTitle}>Dia de preferência</Text>
              
              {/*<TextInput style={styles.input} placeholder="Dias da Semana" value={dados.diaPreferenciaCliente || ""} onChangeText={(text) => setDados({ ...dados, diaPreferenciaCliente: text })} />*/}

              <FlatList
                  data={listaDiasSemana}
                  keyExtractor={(item) => item}
                  contentContainerStyle={{ width: "100%" }}
                  renderItem={({ item }) => (
                    <TouchableOpacity
                      style={[
                        styles.specialityItem,
                        diaPreferenciaCliente.includes(item) && styles.selectedSpeciality,
                      ]}
                      onPress={() => handleSelectDiaPreferenciaCliente(item)}
                    >
                      <Text style={styles.specialityText}>{item}</Text>
                    </TouchableOpacity>
                  )}
              />
              
              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_dia_preferencia_cliente", dados)} width={'100%'}/>
              
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
              <Text style={styles.sectionTitle}>Turno de preferência</Text>
              
              {/*<TextInput style={styles.input} placeholder="Turno" value={dados.turnoPreferenciaCliente || ""} onChangeText={(text) => setDados({ ...dados, turnoPreferenciaCliente: text })} />*/}

              <FlatList
                data={listaTurnos}
                keyExtractor={(item) => item}
                contentContainerStyle={{ width: "100%" }}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={[
                      styles.specialityItem,
                      turnoPreferenciaCliente.includes(item) && styles.selectedSpeciality,
                    ]}
                    onPress={() => handleSelectTurnoPreferenciaCliente(item)}
                  >
                    <Text style={styles.specialityText}>{item}</Text>
                  </TouchableOpacity>
                )}
              />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_turno_preferencia_cliente", dados)} width={'100%'}/>
              
              <TouchableOpacity onPress={() => setStep(4)} style={styles.prevButton}>
                <Text style={styles.buttonText}>← Voltar</Text>
              </TouchableOpacity>
            </View>
            
          )}

          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
        </>
        
      )}

      
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    backgroundColor: "#f9f9f9",
    width:'100%',
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    textAlign: "center",
    marginBottom: 20,
  },
  section: {
    width: "100%",
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 10,
    alignItems: "center",
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: "bold",
    marginBottom: 10,
  },
  input: {
    width: "100%",
    height: 40,
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 5,
    paddingLeft: 10,
    marginBottom: 10,
  },
  mensagem: {
    marginTop: 10,
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
  },
  prevButton: {
    marginTop: 10,
    padding: 10,
    backgroundColor: "#ff5d4b",
    borderRadius: 10,
    width: "100%",
    alignItems: "center",
  },
  nextButton: {
    marginTop: 10,
    padding: 10,
    backgroundColor: "#2196F3",
    borderRadius: 10,
    width: "100%",
    alignItems: "center",
  },
  button: {
    width: "100%",
    backgroundColor: "#08c8f8",
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 10,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#fff",
  },
  specialityItem: {
    padding: 10,
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 8,
    marginBottom: 10,
    width:'100%',
  },
  selectedSpeciality: {
    backgroundColor: "#08c8f8",
    color:'white',
  },
  specialityText: {
    fontSize: 16,
    color: "#081828",
    textAlign: "center",
  },
  
});


export default ConsultarDadosClienteScreen;
