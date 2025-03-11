import React from "react";
import { View, Text, TouchableOpacity, StyleSheet } from "react-native";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { signOut } from "firebase/auth";
import { auth } from "../src/firebaseConfig";

const SessaoRestritaClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {

  const handleSignOut = async () => {
    try {
      await signOut(auth);
      //navigation.navigate("Home");
      navigation.reset({
        index: 0,
        routes: [{ name: "Home" }]
      });
    } catch (error) {
      console.error("Erro ao sair:", error);
    }
  };
  
  return (
    <View style={styles.containerMain}>

      <View style={styles.container}>
        <View>
          <Text style={styles.title}>Transforme sua clínica com inteligência e inovação!</Text>
          <Text style={styles.description}>Gerencie agendamentos, fidelize pacientes e maximize seus resultados – tudo em um só lugar.</Text>
        </View>

        <View style={styles.sectionButton}>
          <CustomButton title="📋 Completar cadastro" customStyle={{
            text: { textAlign: 'left' }
            }} onPress={() => navigation.navigate("DadosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="🔍 Consultar Dados" onPress={() => navigation.navigate("ConsultarDadosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="👨🏽‍⚕️ Cadastrar médicos" onPress={() => navigation.navigate("CadastrarMedicos")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="📅 Sugestão atendimentos" onPress={() => navigation.navigate("SugestaoServicosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="🏥 Consultas agendadas" onPress={() => navigation.navigate("AgendamentosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="🎁 Programa de Benefícios" onPress={() => navigation.navigate("SobreProgramaClinicasBeneficios")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

          <CustomButton title="🚪 Sair" onPress={handleSignOut} backgroundColor="#ff5d4b" textColor="#fff" width={'100%'}/>
        </View>
      </View>
      <Footer textColor="#024059"/>
    </View>
    
  );  
};

const styles = StyleSheet.create({
  containerMain: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    width: '100%'
  },
  container: {
    flex: 1,
    justifyContent: "flex-start",
    alignItems: "flex-start",
    padding: 20,
    width: '100%'
  },
  title: {
    fontSize: 22,
    fontWeight: "bold",
    marginBottom: 10,
    color: '#024059',
    justifyContent:'flex-start'
  },
  description: {
    fontSize: 14,
    marginBottom: 20,
    color: '#024059',
    fontWeight: 'semibold',
  },
  button: {
    flexDirection: 'row', 
    alignItems: 'center', 
    justifyContent: 'flex-start', 
  },
  sectionButton: {
    width: '100%',
    justifyContent: 'center',
    alignItems: 'center',
    marginVertical: 20,
  },
});

export default SessaoRestritaClinicaScreen;
