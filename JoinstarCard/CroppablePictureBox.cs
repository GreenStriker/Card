using System;
using System.Drawing;
using System.Windows.Forms;


namespace JoinstarCard
{
    public partial class CroppablePictureBox : PictureBox
    {

        private Rectangle selectionArea = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private Point selectionStartPoint, dragPreviousPoint;
        private enum CroppingState { NONE, SELECTING, RESIZING, DRAGGING };
        private CroppingState croppingState = CroppingState.NONE;
        private enum DragCorner { NE, NW, SE, SW, NONE }; // the corners of the selection area used for "resize dragging"
        private const int DRAG_CORNER_DELTA_X = 20;
        private const int DRAG_CORNER_DELTA_Y = 20;

        public CroppablePictureBox()
        {
            InitializeComponent();
        }

        // draw selection
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Image != null) // Draw the selection area
            {
                if (IsSelectionAreaPresent())
                {
                    pe.Graphics.FillRectangle(selectionBrush, selectionArea);
                }
            }
        }

        // start selection/resizing/moving
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (IsSelectionAreaPresent()) // we have selected area
            {
                if (!selectionArea.Contains(e.Location))
                { // we are outside of selected => erase current selection
                    selectionArea = new Rectangle(0, 0, 0, 0);
                    Invalidate();
                    croppingState = CroppingState.NONE;
                    return;
                }
                else // we are within the selection area
                {
                    DragCorner dc = PointAtDragCorner(selectionArea, e.Location);
                    if (dc != DragCorner.NONE)
                    { // we are at a corner of selection => we start resizing
                        croppingState = CroppingState.RESIZING; // it is a special drag, where the corners are pulled for resizing
                        dragPreviousPoint = e.Location; // initial previous drag location
                    }
                    else
                    { // we are inside selection => we start dragging
                        croppingState = CroppingState.DRAGGING;
                        dragPreviousPoint = e.Location; // initial previous drag location
                    }
                }
            }
            else // we just start selecting
            {
                croppingState = CroppingState.SELECTING;
                selectionStartPoint = e.Location; // initial selection area location
            }
        }

        // update selection area while on move
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // set a cursor properly
            DragCorner dragCorner = DragCorner.NONE;
            if (selectionArea.Contains(e.Location)) // we are within selection area
            {
                dragCorner = PointAtDragCorner(selectionArea, e.Location);
                switch (dragCorner)
                {
                    case DragCorner.NE: Cursor.Current = Cursors.PanNE; break;
                    case DragCorner.NW: Cursor.Current = Cursors.PanNW; break;
                    case DragCorner.SE: Cursor.Current = Cursors.PanSE; break;
                    case DragCorner.SW: Cursor.Current = Cursors.PanSW; break;
                    default: Cursor.Current = Cursors.Hand; break; // DragCorner.NONE
                }
                Application.DoEvents();
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }

            // handle base case
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // do selection area updates based on the current state
            switch (croppingState) {
                case CroppingState.SELECTING:
                    {
                        // update selection area according to start and temporary end point (e.Location)
                        selectionArea.Location = new Point(
                            Math.Min(selectionStartPoint.X, e.Location.X),
                            Math.Min(selectionStartPoint.Y, e.Location.Y));
                        selectionArea.Size = new Size(
                            Math.Abs(selectionStartPoint.X - e.Location.X),
                            Math.Abs(selectionStartPoint.X - e.Location.X)*4/3);
                        Invalidate();
                        break;
                    }
                case CroppingState.DRAGGING:
                    {
                        // update selection area position with offset from dragging
                        int dX = e.Location.X - dragPreviousPoint.X;
                        int dY = e.Location.Y - dragPreviousPoint.Y;
                        selectionArea.Offset(dX, dY);
                        dragPreviousPoint = e.Location; // save for next move
                        Invalidate();
                        break;
                    }
                case CroppingState.RESIZING: // special combination of SELECTING&DRAGGING: both size and location must be updated
                    {
                        int dX = e.Location.X - dragPreviousPoint.X;
                        int dY = e.Location.Y - dragPreviousPoint.Y; // we are only using dX here, as we are making square avatars currently
                        switch (dragCorner)
                        {
                            case DragCorner.NE:
                                {
                                    selectionArea.Size = new Size(
                                       selectionArea.Width + dX,
                                       selectionArea.Height + dX);
                                    selectionArea.Offset(0, -dX);
                                    break;
                                }
                            case DragCorner.NW:
                                {
                                    selectionArea.Size = new Size(
                                       selectionArea.Width - dX,
                                       selectionArea.Height - dX);
                                    selectionArea.Offset(dX, dX);
                                    break;
                                }
                            case DragCorner.SE:
                                {
                                    selectionArea.Size = new Size(
                                       selectionArea.Width + dX,
                                       selectionArea.Height + dX);
                                    selectionArea.Offset(0, 0);
                                    break;
                                }
                            case DragCorner.SW:
                                {
                                    selectionArea.Size = new Size(
                                        selectionArea.Width - dX,
                                        selectionArea.Height - dX);
                                    selectionArea.Offset(dX, 0);
                                    break;
                                }
                        }
                        dragPreviousPoint = e.Location;
                        Invalidate();
                        break;
                    }
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            croppingState = CroppingState.NONE;
        }

        // helper methods

        private Boolean IsSelectionAreaPresent()
        {
            return selectionArea != null && selectionArea.Width > 0 && selectionArea.Height > 0;
        }

        private void clearSelectionArea() {
            selectionArea = new Rectangle(0, 0, 0, 0);
            Invalidate();
            croppingState = CroppingState.NONE;
        }

        private DragCorner PointAtDragCorner(Rectangle rect, Point p) // it is simplier to calculate with inner rect, so we can use the same drag action
        {
            // check whether we are the any of the internal corners of the specified rect
            // internal corners are specified by deltaX and deltaY values

            Rectangle innerRect = new Rectangle(rect.Left + DRAG_CORNER_DELTA_X, rect.Top + DRAG_CORNER_DELTA_Y, rect.Width - 2 * DRAG_CORNER_DELTA_X, rect.Height - 2 * DRAG_CORNER_DELTA_Y);

            if (Math.Abs(innerRect.Bottom - p.Y) < DRAG_CORNER_DELTA_Y && Math.Abs(innerRect.Right - p.X) < DRAG_CORNER_DELTA_X)
            {
                return DragCorner.SE; // South East / BOTTOM-RIGHT
            }
            if (Math.Abs(innerRect.Bottom - p.Y) < DRAG_CORNER_DELTA_Y && Math.Abs(innerRect.Left - p.X) < DRAG_CORNER_DELTA_X)
            {
                return DragCorner.SW; // South East / BOTTOM-LEFT
            }
            if (Math.Abs(innerRect.Top - p.Y) < DRAG_CORNER_DELTA_Y && Math.Abs(innerRect.Right - p.X) < DRAG_CORNER_DELTA_X)
            {
                return DragCorner.NE; // North East / TOP-RIGHT
            }
            if (Math.Abs(innerRect.Top - p.Y) < DRAG_CORNER_DELTA_Y && Math.Abs(innerRect.Left - p.X) < DRAG_CORNER_DELTA_X)
            {
                return DragCorner.NW; // North West / TOP-LEFT
            }
            return DragCorner.NONE;
        }

        // Bitmap, image handler methods

        public new Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
                clearSelectionArea();
            }
        }

        public Bitmap GetCroppedBitmap()
        {
            if (this.Image == null || !IsSelectionAreaPresent())
            {
                return null; // throw new Exception("No selection yet. Select an area first.");
            }
            Bitmap sourceBitmap = new Bitmap(this.Image);
           
            if ((this.Height - this.Image.Height) > 0 || (this.Width - this.Image.Width) > 0)
            {
                selectionArea.Offset(new Point((this.Width - this.Image.Width)/2, (this.Image.Height - this.Height)/2));
            }
            Bitmap croppedBitmap = sourceBitmap.Clone(selectionArea, sourceBitmap.PixelFormat);

            

            sourceBitmap.Dispose(); 
            return croppedBitmap;
        }
    }
}