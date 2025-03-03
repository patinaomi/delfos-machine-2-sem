import React from "react";
import { TouchableOpacity, Text, StyleSheet, DimensionValue, View } from "react-native";

type CustomButtonProps = {
  title: string;
  onPress: () => void;
  backgroundColor?: string;
  textColor?: string;
  width: DimensionValue;
}

const CustomButton: React.FC<CustomButtonProps> = ({ 
  title, onPress, 
  backgroundColor = "#08c8f8", 
  textColor = "#081828",
  width = "100%" }) => {

  return (
    <TouchableOpacity style={[styles.button, { backgroundColor, width }  ]} onPress={onPress}>
        <Text style={[styles.buttonText, {color: textColor}]}>{title}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    backgroundColor: "#08c8f8",
    paddingVertical: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 20,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
  },
});

export default CustomButton;
