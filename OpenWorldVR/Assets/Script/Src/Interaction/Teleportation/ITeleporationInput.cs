using UnityEditor;
using UnityEngine;

namespace Assets.Script.Src.Interaction.Teleportation
{
    public interface ITeleporationInput
    {
        void Update(float teleportationMultiplier);
        void Grip(object sender, ClickedEventArgs eventArgs);
        void UnGrip(object sender, ClickedEventArgs eventArgs);
        void CloseThumb(object sender, ClickedEventArgs eventArgs);
        void OpenThumb(object sender, ClickedEventArgs eventArgs);
        void CloseIndex(object sender, ClickedEventArgs eventArgs);
        void OpendIndex(object sender, ClickedEventArgs eventArgs);
        void Teleport(object sender, ClickedEventArgs eventArgs);
    }
}
