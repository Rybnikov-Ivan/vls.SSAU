using SSAU.vls.FIRST.lr.ExportToExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace SSAU.vls.FIRST.lr.SendToController
{
    /// <summary>
    /// Отправка данных на контроллер
    /// </summary>
    public class SendToController
    {
        public static SerialPort port = new SerialPort("COM3", 9600);
        static int timeout = 3000;

        public static void Send(ComModel comModel)
        {
            string message = comModel.Com_1.ToString() + "A" +
                             comModel.Com_2.ToString() + "B" +
                             comModel.Com_3.ToString() + "C" +
                             comModel.Com_4.ToString() + "D" +
                             comModel.Com_5.ToString() + "E" +
                             comModel.Com_6.ToString() + "F" +
                             comModel.Com_7.ToString() + "G" +
                             comModel.Com_8.ToString() + "H" +
                             "1234";

            if (port.IsOpen == false)
            {
                port.Open();
                port.ReadTimeout = timeout;
            }


            // Если порт открыт
            if (port.IsOpen)
            {
                // Отправляем сообщение
                port.Write(message);
                // Ждём получения данных
                int seconds = 0;
                while ((port.BytesToRead <= 0) && (seconds <= ((timeout / 1000) - 1)))
                {
                    Thread.Sleep(1000);
                    seconds++;
                }
                // Если и после истечения таймаута данные не поступили, выдаём ошибку
                if (port.BytesToRead <= 0)
                {
                    throw new Exception("Нет ответа от микроконтроллера! Время ожидания истекло!");
                }
                else
                {
                    // Если данные поступили пытаемся их прочитать
                    try
                    {
                        string returnedMesssage = port.ReadExisting();

                    }
                    catch (TimeoutException ex)
                    {
                        throw new Exception("Невозможно прочитать ответ микроконтроллера! Время выполнения операции истекло!");
                    }
                }
            }
            else
            {
                throw new Exception("Порт закрыт!");
            }
        }
    }   

}
