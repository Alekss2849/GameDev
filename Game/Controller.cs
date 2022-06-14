namespace Game
{
    public class Controller
    {
        private Model model;
        private View view;

        public Controller(View view, Model model)
        {
            this.view = view;
            this.model = model;
            view.MoveAction += model.moveAction;
            view.WallAction += model.placeWall;
        }
    }
}
