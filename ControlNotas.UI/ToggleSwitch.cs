using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ControlNotas.UI
{
    public class ToggleSwitch : CheckBox // Nuestra clase Toggle Switch extiende o hereda de checkbox
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color Oncolor { get; set; } = Color.FromArgb(124, 58, 237); // OnColor; darle un color especifico de rgb al control por defecto (Oncolor: cuando el switch esta encendido)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color OffColor { get; set; } = Color.Gray; // Offcolor, gris por defecto cuando el switch esta apagado
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color KnobColor { get; set; } = Color.White; // Knobcontrol, blanco por defecto siempre. Knob = pelotita o el control que se mueve para ejecutar la accion

        public ToggleSwitch() // Constructor
        {
            SetStyle(ControlStyles.UserPaint, true); // Le decimos al sistema que nosotros dibujamos el control manualmente, no windows.
            MinimumSize = new Size(50,26); // Tamano minimo es de 50*26 px
            Size = new Size(50, 26); // Tamano por defecto 50*26

            //Agregar para eliminar el fondo negro del rectangulo
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e) // Este es el metodo que dibuja el control toggleswitch.
        {
            var g = e.Graphics; // e.ghraphics = canvas o lienzo donde se dibuja el control
            g.SmoothingMode = SmoothingMode.AntiAlias; // Activar el suavizado de bordes, sin esto los bordes se pueden ver pixeleados
            // Todo esto es lo que define el tamano del control. Son variables cortas como h y w para no repetir Height y Width.
            int h = Height;
            int w = Width;
            int radius = h / 2;
            //Fondo: con forma de pildora
            var bgColor = Checked ? Oncolor : OffColor; // Elegir el color de fondo dependiendo de si el control esta activado o no (? : operador ternario que funciona como un if)
            
            // Dibujar la forma de la pildora del control
            using (var brush = new SolidBrush(bgColor))
            {
                g.FillEllipse(brush, 0, 0, h, h); // Semicirculo izquierdo
                g.FillEllipse(brush, w - h, 0, h, h); // Semicirculo derecho
                g.FillRectangle(brush, radius, 0, w - h, h); // Rectangulo del medio
            }

            // Dibujamos la bolita blanca del control (la que vamos a mover)
            // Bolita que se mueve segun el checked (si se activa o desactiva el control)
            int knobSize = h - 4; // Definir el tamano de la bola (4 px menos que el alto del ellipse)
            int knobX = Checked ? w - knobSize - 2 : 2; // Le ponemos la posicion horizontal (eje x), utilizando un operador ternario segun si esta checked o no

            using (var brush = new SolidBrush(KnobColor))
            { g.FillEllipse(brush, knobX, 2, knobSize, knobSize); } // Dibujar la bolita en la posicion asignada
        }
    }
}
