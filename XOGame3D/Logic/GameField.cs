
using System.Collections.Generic;
using System.Linq;
using XOGame3D.Enum;

namespace XOGame3D.Logic
{
    internal class GameField : Area<PartField>
    {
        public List<PartField> Fields => _cells.ToList();

        PartField _activeField;//перенести это свойств в PartField, чтобы оно заполнялась на основе того, куда был сделан первый ход ну или придумать др. способ...
        public PartField NextField
        {
            get
            {
                if (_activeField == null) return null;
                var last = _activeField.LastCell;
                var next = _cells.Where(x => x.Ox == last.Ox && x.Oy == last.Oy)
                    .FirstOrDefault();
                if (next.Crowded) return null;
                _activeField = next;
                return next;
            }
        }


        public GameField():base()//Конструкторы не передаются производному классу при наследовании, т.е. нужно принудительно вызывать констрктор базавого класса
        {
            _cells.ForEach(x => x.SetWinner += FieldSetState);//сюда можно будет добавить обработку событий  для первого хода.
        }

        /// <summary>
        /// Возвращает все поля, которые ещё можно заполнять(необходимо,если NextField == null)
        /// </summary>
        /// <returns></returns>
        public List<PartField> GetAvailableFields()
            => _cells.Where(x => !x.Crowded).ToList();

        /// <summary>
        /// При определении победителя в поле, этому полю устанавливается статус победителя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldSetState(object sender, System.EventArgs e)
        {
             var field = sender as PartField;
            field.State = field.Winner;
        }

       
    }
}
