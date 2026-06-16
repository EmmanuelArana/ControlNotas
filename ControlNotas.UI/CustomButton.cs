using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace ControlNotas.UI
{
    public class CustomButton : Button // Nuestra clase CustomButton extiende o hereda de Button
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BackColorNormal { get; set; } = Color.FromArgb(36, 180, 251); // Color de fondo normal (#24b4fb)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BackColorHover { get; set; } = Color.FromArgb(0, 113, 226); // Color de fondo cuando el mouse esta encima (#0071e2)
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColorButton { get; set; } = Color.FromArgb(36, 180, 251); // Color del borde, igual al fondo normal por defecto
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color IconAndTextColor { get; set; } = Color.White; // Color del icono y del texto, blanco por defecto
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderRadius { get; set; } = 14; // Radio de las esquinas redondeadas (equivalente al border-radius: 0.9em del css)

        private bool isHovering = false; // Bandera para saber si el mouse esta encima del boton
        private bool isPressed = false; // Bandera para saber si el boton esta presionado

        public CustomButton() // Constructor
        {
            SetStyle(ControlStyles.UserPaint, true); // Le decimos al sistema que nosotros dibujamos el control manualmente, no windows
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // Evitar que windows pinte el fondo por su cuenta antes de nuestro OnPaint
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); // Permitir fondo transparente para que se vea el padre detras
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // Doble buffer para evitar parpadeo al redibujar

            FlatStyle = FlatStyle.Flat; // Estilo plano, sin relieve por defecto de windows
            FlatAppearance.BorderSize = 0; // Sin borde nativo, el borde lo dibujamos nosotros manualmente
            BackColor = Color.Transparent; // Fondo transparente, lo pintamos nosotros en OnPaint
            ForeColor = IconAndTextColor; // Color de texto por defecto
            Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Fuente en negrita, similar a font-weight:600 del css
            Cursor = Cursors.Hand; // Cursor de mano al pasar por encima, igual a cursor:pointer del css
            MinimumSize = new Size(90, 40); // Tamano minimo del boton
            Size = new Size(120, 42); // Tamano por defecto
            Text = "Create"; // Texto por defecto, igual al ejemplo original

            // Eventos para detectar cuando el mouse entra, sale o presiona el boton (equivalente al :hover del css)
            MouseEnter += (s, e) => { isHovering = true; Invalidate(); };
            MouseLeave += (s, e) => { isHovering = false; isPressed = false; Invalidate(); };
            MouseDown += (s, e) => { isPressed = true; Invalidate(); };
            MouseUp += (s, e) => { isPressed = false; Invalidate(); };
        }

        // Metodo auxiliar que construye la figura del rectangulo con esquinas redondeadas usada como fondo del boton
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2; // Diametro del arco de cada esquina

            path.AddArc(rect.X, rect.Y, d, d, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90); // Esquina superior derecha
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); // Esquina inferior derecha
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); // Esquina inferior izquierda
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e) // Este es el metodo que dibuja el boton manualmente
        {
            var g = e.Graphics; // e.Graphics = canvas o lienzo donde se dibuja el control
            g.SmoothingMode = SmoothingMode.AntiAlias; // Activar el suavizado de bordes para que no se vean pixeleados
            g.Clear(BackColor); // Limpiar el fondo (transparente, respeta el color del padre)

            // Area del boton, dejando 1px de margen para que el borde no se recorte en los bordes del control
            Rectangle rect = new Rectangle(1, 1, Width - 2, Height - 2);

            // Elegir el color de fondo segun el estado actual: presionado, hover o normal (igual al :hover del css)
            Color currentBackColor = isPressed
                ? ControlPaint.Dark(BackColorHover, 0.1f)
                : (isHovering ? BackColorHover : BackColorNormal);

            using (var path = GetRoundedRectPath(rect, BorderRadius))
            {
                // Dibujar el fondo del boton con la forma de rectangulo redondeado
                using (var bgBrush = new SolidBrush(currentBackColor))
                {
                    g.FillPath(bgBrush, path);
                }

                // Dibujar el borde del boton (equivalente al border: 2px solid #24b4fb del css)
                using (var borderPen = new Pen(BorderColorButton, 2))
                {
                    g.DrawPath(borderPen, path);
                }
            }

            // Medir el texto para poder centrarlo en el boton
            SizeF textSize = g.MeasureString(Text, Font);
            float startX = (Width - textSize.Width) / 2f; // Posicion en X para centrar el texto
            float centerY = Height / 2f; // Centro vertical del boton

            // Dibujar el texto del boton (ej: "Create", "Eliminar", "Editar")
            using (var textBrush = new SolidBrush(IconAndTextColor))
            {
                float textY = centerY - textSize.Height / 2f;
                g.DrawString(Text, Font, textBrush, startX, textY);
            }
        }

        protected override void OnResize(EventArgs e) // Redibujar el boton cada vez que cambia de tamano
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}