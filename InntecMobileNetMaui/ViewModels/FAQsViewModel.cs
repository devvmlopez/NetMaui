using InntecMobileNetMaui.RendererControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InntecMobileNetMaui.Models.Enumeradores;

namespace InntecMobileNetMaui.ViewModels
{
    public class FAQsViewModel : BaseViewModel
    {
        public class faq : BaseViewModel
        {
            public string title { get; set; }
            public string description { get; set; }
            private bool _isVisible = false;
            public bool isVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }
        }
        private enumFAQs tipo;
        private double _altoTarjeta = 50;
        public double altoTarjeta { get => _altoTarjeta; }

        public ObservableCollection<faq> Lst_Faq { get; set; }

        public Command LoadFaqCommand;
        public Command AccordingCommand { set; get; }

        public FAQsViewModel(enumFAQs tipo)
        {
            this.tipo = tipo;
            Lst_Faq = new ObservableCollection<faq>();
            LoadFaqCommand = new Command(() => ExecuteLoadFaqCommand());
            AccordingCommand = new Command((item) => ExecuteAccordingComman((List<object>)item));

        }

        private void ExecuteAccordingComman(List<object> parameter)
        {
            if (parameter[0] == null)
                return;

            According item = parameter[0] as According;
            string title = parameter[1].ToString();

            faq item_faq = Lst_Faq.Where(i => i.title == title).FirstOrDefault();

            int index = Lst_Faq.IndexOf(item_faq);
            item_faq.isVisible = !item_faq.isVisible;

            Lst_Faq.Remove(item_faq);
            Lst_Faq.Insert(index, item_faq);

            item.IsOpen = !item.IsOpen;
        }

        private void ExecuteLoadFaqCommand()
        {
            Lst_Faq.Clear();

            switch (tipo)
            {
                case enumFAQs.COMBUSTIBLE:
                    Title = "FAQ Combustibles";
                    #region COMBUSTIBLE
                    FAQsGenerales();
                    Lst_Faq.Add(new faq
                    {
                        title = "7.	¿Quedo hacer si no recuerdo o extravío el NIP de mi tarjeta COMBUSTIBLE?",
                        description = "Este NIP no puede ser recuperado, por lo tanto, es necesario solicitar la reposición de su monedero electrónico."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "8.	¿Qué puedo comprar con mi monedero de COMBUSTIBLE?",
                        description = "Exclusivamente combustible en estaciones de servicio. No es recomendable comprar ningún tipo de producto ya que esto afectara el control del rendimiento del vehículo."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "9.	¿Cómo activo mi tarjeta para la carga y compra de combustible?",
                        description = "1. Ingresamos al botón de tarjeta" + Environment.NewLine +
                                        "2. Seleccionamos nueva solicitud" + Environment.NewLine +
                                        "3. Llenamos el formulario solicitado" + Environment.NewLine +
                                        "4. Y damos clic en activar tarjeta." + Environment.NewLine +
                                        "5. Al ser aceptada la solicitud, el plástico se activa por un lapso de 20 minutos para generar el pago del combustible.Después de este tiempo, la tarjeta vuelve a bloquearse en automático."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "10. ¿Si necesito cargar combustible nuevamente que debo hacer?",
                        description = "Es necesario generar nuevamente el proceso de activación."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "11. ¿Cuáles son los motivos por los que no puedo activar mi tarjeta si estoy generando el proceso de activación?",
                        description = "1. Es probable que este ingresando mal el kilometraje de su vehículo (menor al último registrado)" + Environment.NewLine +
                                        "2. No leer el QR asignado a su vehículo." + Environment.NewLine +
                                        "3. No contar con datos en su celular para la conexión adecuada a nuestro sistema" + Environment.NewLine +
                                        "4. Contar con algún tipo de restricción en su plástico." + Environment.NewLine +
                                        "5. Si su plástico cuenta con algún tipo de bloqueo, en este caso comunicarse directamente con su administrador."
                    });
                    #endregion
                    break;
                case enumFAQs.DESPENSA:
                    Title = "FAQ Despensa";
                    #region DESPENSA
                    FAQsGenerales();
                    Lst_Faq.Add(new faq
                    {
                        title = "7.	¿Dónde puedo comprar con mi tarjeta CARNET?",
                        description = "En cualquier establecimiento que cuente con terminal bancaria, siempre y cuando se encuentre en territorio Nacional."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "8.	¿Qué puedo comprar con mi monedero electrónico de DESPENSA?",
                        description = "Puede adquirir todo producto que eleve su calidad de vida, estos no incluyen, alcohol, tabaco, giros negros ni combustibles."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "9.	¿Quedo hacer si no recuerdo o extravío el NIP de mi tarjeta DESPENSA?",
                        description = "Este NIP no puede ser recuperado, por lo tanto, es necesario solicitar la reposición de su monedero electrónico."
                    });
                    break;
                #endregion
                case enumFAQs.PREMIUM:
                    Title = "FAQ Premium";
                    #region PREMIUM
                    Lst_Faq.Add(new faq
                    {
                        title = "1.	¿Dónde puedo comprar con mi tarjeta MasterCard?",
                        description = "En cualquier comercio que cuente con terminal bancaria. Estas tarjetas tienen aceptación a nivel Internacional."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "2.	¿Cuál es la comisión que cobran los cajeros de otros bancos al retirar efectivo?",
                        description = "Esta comisión depende de cada banco, ya que nosotros no determinamos dicha cantidad."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "3.	¿Qué debo hacer si extravío mi tarjeta INNTEC?",
                        description = "Es necesario comunicarse directamente al área de call center (33 9690 7701), también es recomendable bloquear la tarjeta directamente en su APP Mi Inntec Móvil."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "4.	¿Qué debo hacer si el cajero electrónico retiene mi tarjeta MasterCard?",
                        description = "Primero bloquear la tarjeta en su app y después comunicarse a nuestro call center para que generen el proceso de reposición de tarjeta."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "5.	¿Qué debo hacer con mi tarjeta de reposición?",
                        description = "Es necesario agregarla a su registro para que pueda generar la activación y así mismo tener el control de su plástico nuevo."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "6.	¿Cómo activo mi tarjeta INNTEC en la app?",
                        description = "En la imagen de la tarjeta, se encuentra un botón azul de lado izquierdo, el cual solo deberá dar clic para activarla y confirmar el cambio de estatus."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "7.	¿Como puedo realizar el cambio de mi NIP en mi app?",
                        description = "El nip que viene al reverso de su plástico en una etiqueta blanca NO puede ser cambiado. Este tendrá que utilizarlo para confirmar sus compras y retiros de efectivo ya sea directamente en cajeros ATM o en cajas de tiendas de autoservicio (Cash Back)."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "8.	¿Qué debo hacer si no recuerdo o extravío el NIP de mi tarjeta MasterCard?",
                        description = "Es necesario mandar correo a CAT@inntecmp.com.mx solicitando la recuperación de su nip."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "9.	¿Qué debo hacer si ingrese 3 veces mal el NIP de mi monedero electrónico de MasterCard?",
                        description = "Le sugerimos no intentarlo una 4ta vez, ya que puede llegar a bloquear su tarjeta y no podrá retirar hasta pasadas las 24 horas posteriores al último intento de retiro. Si detecto que está ingresando el nip incorrecto, le recomendamos solicitar la recuperación de su nip al correo CAT@inntecmp.com.mx o comunicarse a nuestro call center (33 9690 7701)."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "10. ¿Qué debo hacer si no reconozco algún consumo?",
                        description = "Deberá mandar correo a aclaraciones.mastercard@inntecmp.com.mx proporcionando su número de tarjeta y movimiento que no recuerda haber generado. Si la compra no reconocida es por internet es importante que genere el bloqueo inmediato de su tarjeta en su App y reportarla en nuestro call center para generar el proceso de reposición."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "11. ¿Qué debo hacer si un comercio me cobra doble una compra?",
                        description = "Antes de generar alguna aclaración es recomendable esperar de 24 a 72 horas para verificar si el comercio aplica la devolución automática. Después de este tiempo, si no se genera la devolución, es necesario reportarlo al área de aclaraciones."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "12. ¿Cuánto tiempo demora una aclaración de saldo?",
                        description = "El tiempo se determina de acuerdo al estatus en el que se encuentre el cargo, el cual se lo hará saber el área de aclaraciones:" + Environment.NewLine + Environment.NewLine +

                        "Consumo Confirmado: de 45 a 90 días" + Environment.NewLine +
                        "Consumo anulado o denegado: hasta 30 días" + Environment.NewLine +
                        "Consumo en tránsito: hasta 30 días"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "13. ¿Cuánto puedo retirar al día con mi tarjeta MasterCard?",
                        description = "Puede retirar hasta $9,000.00 cada 24 horas, esto dividido en 2 exhibiciones, el primer retiro por hasta $5,600.00 y el segundo por la diferencia."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "14. ¿Cuánto puedo pagar en terminales punto de venta al día?",
                        description = "Hasta $120,000.00 al día dividido en transacciones de máximo $25,000.00 cada 24 horas"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "15. ¿Cuál es la cantidad máxima que puedo pagar o comprar por internet?",
                        description = "Hasta $25,000.00 cada 24 horas"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "16. ¿Motivos por los cuales mi tarjeta INNTEC es rechazada en algunos comercios?",
                        description = "1. Se pueden presentar rechazos por fallas de conexión entre la terminal bancaria y nuestro procesador. " + Environment.NewLine + Environment.NewLine +
                                      "2. Porque la terminal bancaria está leyendo la banda y no el CHIP." + Environment.NewLine +
                                      "3. Por qué está intentando pagar más del saldo con el que cuenta" + Environment.NewLine +
                                      "4. Porque su plástico se encuentra bloqueado en su app." + Environment.NewLine + Environment.NewLine + Environment.NewLine +

                        "En cualquiera de estos casos, le recomendamos primero, consultar en su app su saldo y estatus del plástico y si continúan las fallas intentar más tarde o en otro comercio y reportarlo a nuestro call center para su revisión."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "17. ¿Puedo realizar depósitos a mi tarjeta PREMIUM MC?",
                        description = "Los depósitos a su plástico únicamente los puede generar su empresa, es decir, ningún tarjetahabiente puede generar depósitos a su tarjeta."
                    });
                    #endregion
                    break;
                case enumFAQs.VIATICOS:
                    Title = "FAQ Viaticos";
                    #region VIATICOS
                    Lst_Faq.Add(new faq
                    {
                        title = "1.	¿Dónde puedo comprar con mi tarjeta MasterCard?",
                        description = "En cualquier comercio que cuente con terminal bancaria. Estas tarjetas tienen aceptación a nivel Internacional."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "2.	¿Qué debo hacer si extravío mi tarjeta INNTEC?",
                        description = "Es necesario comunicarse directamente al área de call center (33 9690 7701), también es recomendable bloquear la tarjeta directamente en su APP Mi Inntec Móvil."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "3.	¿Qué debo hacer con mi tarjeta de reposición?",
                        description = "Es necesario agregarla a su registro para que pueda generar la activación y así mismo tener el control de su plástico nuevo."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "4.	¿Cómo activo mi tarjeta INNTEC en la app?",
                        description = "En la imagen de la tarjeta, se encuentra un botón azul de lado izquierdo, el cual solo deberá dar clic para activarla y confirmar el cambio de estatus."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "5.	¿Como puedo realizar el cambio de mi NIP en mi app?",
                        description = "El nip que viene al reverso de su plástico en una etiqueta blanca NO puede ser cambiado. Este tendrá que utilizarlo para confirmar sus compras y retiros de efectivo ya sea directamente en cajeros ATM o en cajas de tiendas de autoservicio (Cash Back)."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "6.	¿Qué debo hacer si no recuerdo o extravío el NIP de mi tarjeta MasterCard?",
                        description = "Es necesario mandar correo a CAT@inntecmp.com.mx solicitando la recuperación de su nip."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "7.	¿Qué debo hacer si no reconozco algún consumo?",
                        description = "Deberá mandar correo a aclaraciones.mastercard@inntecmp.com.mx proporcionando su número de tarjeta y movimiento que no recuerda haber generado. Si la compra no reconocida es por internet es importante que genere el bloqueo inmediato de su tarjeta en su App y reportarla en nuestro call center para generar el proceso de reposición."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "8.	¿Qué debo hacer si un comercio me cobra doble una compra?",
                        description = "Antes de generar alguna aclaración es recomendable esperar de 24 a 72 horas para verificar si el comercio aplica la devolución automática. Después de este tiempo, si no se genera la devolución, es necesario reportarlo al área de aclaraciones."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "9. ¿Cuánto tiempo demora una aclaración de saldo?",
                        description = "El tiempo se determina de acuerdo al estatus en el que se encuentre el cargo, el cual se lo hará saber el área de aclaraciones:" + Environment.NewLine + Environment.NewLine +

                        "Consumo Confirmado: de 45 a 90 días" + Environment.NewLine +
                        "Consumo anulado o denegado: hasta 30 días" + Environment.NewLine +
                        "Consumo en tránsito: hasta 30 días"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "10. ¿Cuánto puedo pagar en terminales punto de venta al día?",
                        description = "Hasta $120,000.00 al día dividido en transacciones de máximo $25,000.00 cada 24 horas"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "11. ¿Cuál es la cantidad máxima que puedo pagar o comprar por internet?",
                        description = "Hasta $25,000.00 cada 24 horas"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "12. ¿Motivos por los cuales mi tarjeta INNTEC es rechazada en algunos comercios?",
                        description = "1. Se pueden presentar rechazos por fallas de conexión entre la terminal bancaria y nuestro procesador. " + Environment.NewLine + Environment.NewLine +
                                      "2. Porque la terminal bancaria está leyendo la banda y no el CHIP." + Environment.NewLine +
                                      "3. Por qué está intentando pagar más del saldo con el que cuenta" + Environment.NewLine +
                                      "4. Porque su plástico se encuentra bloqueado en su app." + Environment.NewLine + Environment.NewLine + Environment.NewLine +

                        "En cualquiera de estos casos, le recomendamos primero, consultar en su app su saldo y estatus del plástico y si continúan las fallas intentar más tarde o en otro comercio y reportarlo a nuestro call center para su revisión."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "13. ¿Cómo puedo eliminar mi comprobante o factura de un cargo equivocado?",
                        description = "No es posible que tú lo puedas eliminar, para ello es necesario comunicarte con el administrador de tu empresa para que el genere el retiro del documento y lo puedas cargar de forma correcta nuevamente."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "14. ¿Qué hago si la factura de mi consumo no se carga en mi app?",
                        description = "Es necesario comunicarse directamente con el administrador de su empresa."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "15. ¿A que correo electrónico tienen que enviar los comercios mis facturas?",
                        description = "Al correo electrónico que se compone de la numeración de tu tarjeta (16 dígitos) y agregando el siguiente dominio @iviaticos.com.mx, ejemplo; 5359000012345678@iviaticos.com.mx"
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "16. ¿Puedo retirar con mi tarjeta en cajero ATM? ",
                        description = "Por el tipo de producto, no es recomendable generar esta operación ya que no hay forma de comprobar ante el SAT la salida de este ingreso y a su vez, tendrá que escanear su ticket de retiro para cargarlo en su app y de esta manera comprobar la salida del efectivo."
                    });
                    #endregion
                    break;
                case enumFAQs.TOKEN:
                    Title = "FAQ Token";
                    #region TOKEN
                    Lst_Faq.Add(new faq
                    {
                        title = "1.	¿Para que necesito el Token de seguridad?",
                        description = "El token es necesario para realizar el registro inicial de tu tarjeta o agregar una nueva tarjeta en tu aplicación."
                    });
                    Lst_Faq.Add(new faq
                    {
                        title = "2.	¿Cómo puedo obtener mi Token de seguridad?",
                        description = "Este token únicamente puede brindártelo tu empresa o la persona que te otorgo el plástico ya que son los únicos con la facultad de generarlos."
                    });
                    #endregion
                    break;
            }

            if (tipo == enumFAQs.COMBUSTIBLE)
            {

            }
        }
        private void FAQsGenerales()
        {
            #region GENERALES
            Lst_Faq.Add(new faq
            {
                title = "1. ¿Cómo activo mi tarjeta INNTEC en la app?",
                description = "En la imagen de la tarjeta, se encuentra un botón azul, el cual solo deberá dar clic para activarla y confirmar el cambio de estatus."
            });

            Lst_Faq.Add(new faq
            {
                title = "2. ¿Qué debo hacer si extravío mi tarjeta INNTEC?",
                description = "Es necesario comunicarse directamente al área de call center (339690 7701), también es recomendable bloquear la tarjeta directamente en su APP Mi Inntec Móvil"
            });

            Lst_Faq.Add(new faq
            {
                title = "3. ¿Qué debo hacer con mi tarjeta de reposición?",
                description = "Es necesario agregarla a su registro para poder generar la activación y así mismo tener el control de su plástico nuevo."
            });
            Lst_Faq.Add(new faq
            {
                title = "4.	¿Qué debo hacer si no reconozco algún consumo?",
                description = "Deberá mandar correo a aclaraciones@inntecmp.com.mx proporcionando su número de tarjeta y movimiento que no reconoce haber generado, si la compra no reconocida es por internet es importante que genere el bloqueo inmediato de su tarjeta en su App."
            });
            Lst_Faq.Add(new faq
            {
                title = "5.	¿Qué debo hacer si un comercio me cobra doble una compra?",
                description = "Antes de generar alguna aclaración es recomendable esperar de 24 a 72 horas para verificar si el comercio aplica la devolución automática. Después de este tiempo, si no se genera la devolución, es necesario reportarlo al área de aclaraciones."
            });
            Lst_Faq.Add(new faq
            {
                title = "6.	¿Qué debo hacer si ingrese 3 veces mal el NIP de mi tarjeta?",//monedero electrónico de COMBUSTIBLE?",
                description = "Una vez bloqueado no podrá activarse de nuevo, por lo tanto, será necesario comunicarse al área de call center y solicitar su reposición de tarjeta."
            });
            #endregion
        }
    }
}
