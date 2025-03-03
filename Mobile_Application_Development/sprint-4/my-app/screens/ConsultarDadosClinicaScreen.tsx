import React, { useState, useEffect } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity, ScrollView, FlatList } from "react-native";
import { doc, getDoc, updateDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";

const ConsultarDadosClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
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

  const handleSelectEspecialidade = (especialidade: string) => {
    setEspecialidades(prevState => {
      if (prevState.includes(especialidade)) {
        return prevState.filter(item => item !== especialidade);
      } else {
        return [...prevState, especialidade];
      }
    });
  };

  const handleSelectDiaSemana = (diaSemana: string) => {
    setDiasSemana(prevState => {
      if (prevState.includes(diaSemana)) {
        return prevState.filter(item => item !== diaSemana);
      } else {
        return [...prevState, diaSemana];
      }
    });
  };

  const handleSelectTurno = (turno: string) => {
    setTurno(prevState => {
      if (prevState.includes(turno)) {
        return prevState.filter(item => item !== turno);
      } else {
        return [...prevState, turno];
      }
    });
  };

  useEffect(() => {
    const buscarDados = async () => {
      try {
        const dadosCadastraisRef = doc(db, "t_dados_cadastrais_clinicas", user.uid);
        const enderecoRef = doc(db, "t_endereco_clinica", user.uid);
        const especilidadesRef = doc(db, "t_especialidades", user.uid);
        const diasPreferenciaRef = doc(db, "t_dias_preferencia_clinicas", user.uid);
        const turnoPreferenciaRef = doc(db, "t_turno_preferencia_clinicas", user.uid);
  
        const dadosCadastraisSnap = await getDoc(dadosCadastraisRef);
        const enderecoSnap = await getDoc(enderecoRef);
        const especialidadesSnap = await getDoc(especilidadesRef);
        const diasPreferenciaSnap = await getDoc(diasPreferenciaRef);
        const turnoPreferenciaSnap = await getDoc(turnoPreferenciaRef);
  
        setDados((prevDados: any) => ({
          ...prevDados, // Mantém os dados existentes
          ...(dadosCadastraisSnap.exists() ? dadosCadastraisSnap.data() : {}),
          ...(enderecoSnap.exists() ? enderecoSnap.data() : {}),
          ...(especialidadesSnap.exists() ? especialidadesSnap.data() : {}),
          ...(diasPreferenciaSnap.exists() ? diasPreferenciaSnap.data() : {}),
          ...(turnoPreferenciaSnap.exists() ? turnoPreferenciaSnap.data() : {}),
        }));

        // Carregar especialidades
        if (especialidadesSnap.exists()) {
          setEspecialidades(especialidadesSnap.data().especialidades || []);
        }

        // Carregar todas as especialidades disponíveis
        const especialidadesDisponiveisRef = doc(db, "t_especialidades", "todos");
        const especialidadesDisponiveisSnap = await getDoc(especialidadesDisponiveisRef);
        
        // Carregar dias da semana
        if (diasPreferenciaSnap.exists()) {
          setDiasSemana(diasPreferenciaSnap.data().diasSemana || []);
        }

        // Carregar todas as especialidades disponíveis
        const diaSemanaDisponiveisRef = doc(db, "t_dias_preferencia_clinicas", "todos");
        const diaSemanaDisponiveisSnap = await getDoc(diaSemanaDisponiveisRef);

        // Carregar turnos
        if (turnoPreferenciaSnap.exists()) {
          setTurno(turnoPreferenciaSnap.data().turno || []);
        }
        
        // Carregar todos os turnos
        const turnoDisponiveisRef = doc(db, "t_turno_preferencia_clinicas", "todos");
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
      const docRef = doc(db, colecao, user.uid);
      await updateDoc(docRef, novosDados);
      setMensagem("✅ Dados atualizados com sucesso!");
    } catch (error) {
      console.error("Erro ao atualizar dados:", error);
      setMensagem("❌ Erro ao atualizar os dados.");
    }
  };

  {/*}
  const handleAddEspecialidade = (especialidade: string) => {
    if (especialidade && !especialidades.includes(especialidade)) {
      const updatedEspecialidades = [...especialidades, especialidade];
      setEspecialidades(updatedEspecialidades);
      atualizarDados("t_especialidades", { especialidades: updatedEspecialidades });
    }
  };

  const handleRemoveEspecialidade = (especialidade: string) => {
    const updatedEspecialidades = especialidades.filter((item) => item !== especialidade);
    setEspecialidades(updatedEspecialidades);
    atualizarDados("t_especialidades", { especialidades: updatedEspecialidades });
  };
  */}

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Meus Dados</Text>

      {loading ? (
        <Text>Carregando...</Text>
      ) : (
        <>
          {step === 1 && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>Dados cadastrais</Text>
              <TextInput style={styles.input} placeholder="Nome" value={dados.nome || ""} onChangeText={(text) => setDados({ ...dados, nome: text })} />
              <TextInput style={styles.input} placeholder="CNPJ" value={dados.cnpj || ""} editable={false} />
              
              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_dados_cadastrais_clinicas", dados)} width={'100%'}/>
              
              <TouchableOpacity onPress={() => setStep(2)} style={styles.nextButton}>
                <Text style={styles.buttonText}>→ Próximo</Text>
              </TouchableOpacity>

            </View>
          )}

          {step === 2 && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>Endereço</Text>
              <TextInput style={styles.input} placeholder="CEP" value={dados.cep || ""} onChangeText={(text) => setDados({ ...dados, cep: text })} />
              <TextInput style={styles.input} placeholder="Estado" value={dados.estado || ""} onChangeText={(text) => setDados({ ...dados, estado: text })} />
              <TextInput style={styles.input} placeholder="Cidade" value={dados.cidade || ""} onChangeText={(text) => setDados({ ...dados, cidade: text })} />
              <TextInput style={styles.input} placeholder="Bairro" value={dados.bairro || ""} onChangeText={(text) => setDados({ ...dados, bairro: text })} />
              <TextInput style={styles.input} placeholder="Rua" value={dados.rua || ""} onChangeText={(text) => setDados({ ...dados, rua: text })} />
              <TextInput style={styles.input} placeholder="Número" value={dados.numero || ""} onChangeText={(text) => setDados({ ...dados, numero: text })} />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_endereco_clinica", dados)} width={'100%'}/>
              
              
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
              
              {/*<FlatList
                data={especialidades}
                keyExtractor={(item, index) => `${index}`}
                renderItem={({ item }) => (
                  <View style={styles.itemContainer}>
                    <Text style={styles.itemText}>{item}</Text>
                    <TouchableOpacity onPress={() => handleRemoveEspecialidade(item)} style={styles.removeButton}>
                      <Text style={styles.removeButtonText}>Remover</Text>
                    </TouchableOpacity>
                  </View>
                )}
                />*/}

              <FlatList
                data={listaEspecialidades}
                keyExtractor={(item) => item}
                contentContainerStyle={{ width: "100%" }}
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

           <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_especialidades", { especialidades })} width={'100%'}/>
           
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
              
              {/*}
              <TextInput style={styles.input} placeholder="Dias da Semana" value={dados.diasSemana || ""} onChangeText={(text) => setDados({ ...dados, diasSemana: text })} />
              */}

              <FlatList
                  data={listaDiasSemana}
                  keyExtractor={(item) => item}
                  contentContainerStyle={{ width: "100%" }}
                  renderItem={({ item }) => (
                    <TouchableOpacity
                      style={[
                        styles.specialityItem,
                        diasSemana.includes(item) && styles.selectedSpeciality,
                      ]}
                      onPress={() => handleSelectDiaSemana(item)}
                    >
                      <Text style={styles.specialityText}>{item}</Text>
                    </TouchableOpacity>
                  )}
              />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_dias_preferencia_clinicas", dados)} width={'100%'}/>
              
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

              {/*}
              <TextInput style={styles.input} placeholder="Turno" value={dados.turno || ""} onChangeText={(text) => setDados({ ...dados, turno: text })} />*/}

              <FlatList
                data={listaTurnos}
                keyExtractor={(item) => item}
                contentContainerStyle={{ width: "100%" }}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    style={[
                      styles.specialityItem,
                      turno.includes(item) && styles.selectedSpeciality,
                    ]}
                    onPress={() => handleSelectTurno(item)}
                  >
                    <Text style={styles.specialityText}>{item}</Text>
                  </TouchableOpacity>
                )}
              />

              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_turno_preferencia_clinicas", dados)} width={'100%'}/>
              
              <TouchableOpacity onPress={() => setStep(4)} style={styles.prevButton}>
                <Text style={styles.buttonText}>← Voltar</Text>
              </TouchableOpacity>
            </View>
          )}

          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
        </>
      )}

      <Footer textColor='#000'/>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    backgroundColor: "#f9f9f9",
    width:'100%'
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
    backgroundColor: "#D32F2F",
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
    minWidth: "100%",
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
  itemContainer: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 5,
    width: "100%",
  },
  itemText: {
    fontSize: 16,
    color: "#333",
  },
  sectionSubTitle: {
    fontSize: 16,
    marginBottom: 10,
    fontWeight: 'bold',
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


export default ConsultarDadosClinicaScreen;
