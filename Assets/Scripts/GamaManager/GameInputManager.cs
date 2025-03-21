using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Left1();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Left2();
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Right1();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Right2();
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Jump1();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    if (UiControl.Instances != null)
        //    {
        //        UiControl.Instances.Jump2();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            UseSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            UseSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (AttackSkillManager.Instance == null)
            {
                return;
            }
            AttackSkillManager.Instance.ChangeSkill();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (PlayerCombatSystem.Instances != null)
            {
                PlayerCombatSystem.Instances.ManualAttack();
            }
        }
        else if (Input.GetKeyUp(KeyCode.M)) 
        {
            if (PlayerCombatSystem.Instances != null)
            {
                PlayerCombatSystem.Instances.StopManualAttack();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InfomationGame.Instances != null)
            {
                InfomationGame.Instances.Pause();
            }
        }

    }

    void UseSkill(int skillNumber)
    {
        if (SupportSkillManager.Instance == null)
        {
            return;
        }
        SupportSkillManager.Instance.ActiveItems(skillNumber);
    }
}
