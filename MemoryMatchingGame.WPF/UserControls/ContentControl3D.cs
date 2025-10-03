using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace MemoryMatchingGame.WPF.UserControls;

/// <summary>
/// https://joshsmithonwpf.wordpress.com/2009/02/23/introducing-contentcontrol3d/
/// </summary>
public class ContentControl3D : ContentControl
{
    #region Fields

    bool _isRotating;
    int _rotationRequests;
    Viewport3D _viewport;

    #endregion // Fields

    #region Constructors

    static ContentControl3D()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ContentControl3D),
            new FrameworkPropertyMetadata(typeof(ContentControl3D)));

        AnimationLengthProperty =
            DependencyProperty.Register(
            "AnimationLength",
            typeof(int),
            typeof(ContentControl3D),
            new UIPropertyMetadata(600, OnAnimationLengthChanged));

        BackContentProperty = DependencyProperty.Register(
            "BackContent",
            typeof(object),
            typeof(ContentControl3D));

        BackContentTemplateProperty = DependencyProperty.Register(
            "BackContentTemplate",
            typeof(DataTemplate),
            typeof(ContentControl3D),
            new UIPropertyMetadata(null));

        IsFrontInViewPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsFrontInView",
            typeof(bool),
            typeof(ContentControl3D),
            new UIPropertyMetadata(true));
        IsFrontInViewProperty = IsFrontInViewPropertyKey.DependencyProperty;
    }

    public ContentControl3D()
    {
        CommandBindings.Add(new CommandBinding(RotateCommand, OnRotateCommandExecuted, OnCanExecuteRotateCommand));
    }

    #endregion // Constructors

    #region Rotate

    public void Rotate()
    {
        if (_isRotating)
        {
            ++_rotationRequests;
            return;
        }
        else
        {
            _isRotating = true;
        }

        if (_viewport != null)
        {
            // Find front rotation
            Viewport2DVisual3D backContentSurface = (Viewport2DVisual3D)_viewport.Children[1];
            RotateTransform3D backTransform = (RotateTransform3D)backContentSurface.Transform;
            AxisAngleRotation3D backRotation = (AxisAngleRotation3D)backTransform.Rotation;

            // Find back rotation
            Viewport2DVisual3D frontContentSurface = (Viewport2DVisual3D)_viewport.Children[2];
            RotateTransform3D frontTransform = (RotateTransform3D)frontContentSurface.Transform;
            AxisAngleRotation3D frontRotation = (AxisAngleRotation3D)frontTransform.Rotation;

            // Create a new camera each time, to avoid trying to animate a frozen instance.
            PerspectiveCamera camera = CreateCamera();
            _viewport.Camera = camera;

            // Create animations.
            DoubleAnimation rotationAnim = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationLength)),
                By = 180
            };
            Point3DAnimation cameraZoomAnim = new Point3DAnimation
            {
                To = new Point3D(0, 0, 2.5),
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationLength / 2)),
                AutoReverse = true
            };
            cameraZoomAnim.Completed += OnRotationCompleted;

            // Start the animations.
            frontRotation?.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotationAnim);
            backRotation?.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotationAnim);
            camera.BeginAnimation(PerspectiveCamera.PositionProperty, cameraZoomAnim);
        }
    }

    void OnRotationCompleted(object sender, EventArgs e)
    {
        IsFrontInView = !IsFrontInView;

        _isRotating = false;

        if (_rotationRequests > 0)
        {
            --_rotationRequests;
            Rotate();
        }
    }

    #endregion // Rotate

    #region Rotate Command

    /// <summary>
    /// Causes the 3D surface to rotate, when executed.
    /// </summary>
    public static readonly RoutedCommand RotateCommand = new();

    void OnRotateCommandExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Rotate();
    }

    void OnCanExecuteRotateCommand(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    #endregion // Rotate Command

    #region Dependency Properties

    #region AnimationLength

    /// <summary>
    /// Gets/sets the number of milliseconds it should take to flip the 3D surface over.
    /// This property cannot be set to a value less than 10.
    /// </summary>
    public int AnimationLength
    {
        get { return (int)GetValue(AnimationLengthProperty); }
        set { SetValue(AnimationLengthProperty, value); }
    }

    public static readonly DependencyProperty AnimationLengthProperty;

    static void OnAnimationLengthChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
    {
        int value = (int)e.NewValue;
        if (value < 10)
        {
            throw new ArgumentOutOfRangeException("AnimationLength", "AnimationLength cannot be less than 10 milliseconds.");
        }
    }

    #endregion // AnimationLength

    #region BackContent

    /// <summary>
    /// Gets/sets the object to display on the back side of the 3D surface.
    /// </summary>
    public object BackContent
    {
        get { return (object)GetValue(BackContentProperty); }
        set { SetValue(BackContentProperty, value); }
    }

    public static readonly DependencyProperty BackContentProperty;

    #endregion // BackContent

    #region BackContentTemplate

    public DataTemplate BackContentTemplate
    {
        get { return (DataTemplate)GetValue(BackContentTemplateProperty); }
        set { SetValue(BackContentTemplateProperty, value); }
    }

    public static readonly DependencyProperty BackContentTemplateProperty;

    #endregion // BackContentTemplate

    #region IsFrontInView

    /// <summary>
    /// Returns true if the front side of the 3D surface is currently in view.  Read-only.
    /// </summary>
    public bool IsFrontInView
    {
        get { return (bool)GetValue(IsFrontInViewProperty); }
        private set { SetValue(IsFrontInViewPropertyKey, value); }
    }

    public static readonly DependencyProperty IsFrontInViewProperty;
    private static readonly DependencyPropertyKey IsFrontInViewPropertyKey;

    #endregion // IsFrontInView

    #endregion // Dependency Properties

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _viewport = (Viewport3D)Template.FindName("PART_Viewport", this);
        if (_viewport != null)
        {
            _viewport.Camera = CreateCamera();
        }
    }

    #endregion // Base Class Overrides

    #region Private Helpers

    PerspectiveCamera CreateCamera()
    {
        return new PerspectiveCamera
        {
            Position = new Point3D(0, 0, 1.2),
            LookDirection = new Vector3D(0, 0, -1),
            FieldOfView = 90
        };
    }

    #endregion // Private Helpers
}