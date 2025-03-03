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
    <View style={styles.container}>
      <Text style={styles.title}>Área Restrita</Text>

      <CustomButton title="📋 Completar cadastro" onPress={() => navigation.navigate("DadosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🔍 Consultar Dados" onPress={() => navigation.navigate("ConsultarDadosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="👨🏽‍⚕️ Cadastrar médicos" onPress={() => navigation.navigate("CadastrarMedicos")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="📅 Agendamentos" onPress={() => navigation.navigate("SugestaoServicosClinica")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🏥 Consultas" onPress={() => navigation.navigate("Consultas")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🎁 Programa de Benefícios" onPress={() => navigation.navigate("Consultas")} backgroundColor="#2196F3" textColor="#ffff" width={'100%'}/>

      <CustomButton title="🚪 Sair" onPress={handleSignOut} backgroundColor="#D32F2F" textColor="#fff" width={'100%'}/>
      
      <Footer textColor="#000"/>
    </View>
    
  );  
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    width: '100%'
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
  },
  button: {
    flexDirection: 'row', 
    alignItems: 'center', 
    justifyContent: 'flex-start', 
  },
});

export default SessaoRestritaClinicaScreen;
