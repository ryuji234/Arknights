using System;
using UnityEngine;

public class SilverAsh : MonoBehaviour
{
    private bool firstSetting = true;
    private bool OnClick = false;
    private float PosX;
    private float PosY;
    private int TileX;
    private int TileY;
    private int direct;
    private void OnMouseDown()
    {
        if (firstSetting)
        {
            OnClick = true;
            PosX = Input.mousePosition.x;
            PosY = Input.mousePosition.y;
            Debug.Log("눌림");
            TileX = (int)SilverAshButton.SetTile.position.x / 4;
            TileY = (int)SilverAshButton.SetTile.position.z / 4;

        }
    }
    private void OnMouseDrag()
    {
        if (OnClick)
        {


            if (Math.Abs(Input.mousePosition.x - PosX) > Math.Abs(Input.mousePosition.y - PosY))
            {
                if (Input.mousePosition.x - PosX > 100)
                {
                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            break;
                        case 2:
                            ClearLeft();
                            break;
                        case 3:
                            ClearUp();
                            break;
                        case 4:
                            ClearDown();
                            break;
                        default:
                            break;
                    }
                    direct = 1;
                    Right();
                }
                else if (Input.mousePosition.x - PosX < -100)
                {
                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            break;
                        case 2:
                            ClearLeft();
                            break;
                        case 3:
                            ClearUp();
                            break;
                        case 4:
                            ClearDown();
                            break;
                        default:
                            break;
                    }
                    Debug.Log("왼쪽을 보고 있다.");
                    direct = 2;
                    Left();
                }
                
            }
            else
            {
                if (Input.mousePosition.y - PosY > 100)
                {
                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            break;
                        case 2:
                            ClearLeft();
                            break;
                        case 3:
                            ClearUp();
                            break;
                        case 4:
                            ClearDown();
                            break;
                        default:
                            break;
                    }
                    direct = 3;
                    Up();

                }
                else if (Input.mousePosition.y - PosY < -100)
                {
                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            break;
                        case 2:
                            ClearLeft();
                            break;
                        case 3:
                            ClearUp();
                            break;
                        case 4:
                            ClearDown();
                            break;
                        default:
                            break;
                    }
                    direct = 4;
                    Down();
                }
                else
                {
                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            break;
                        case 2:
                            ClearLeft();
                            break;
                        case 3:
                            ClearUp();
                            break;
                        case 4:
                            ClearDown();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    private void OnMouseUp()
    {
        if (firstSetting)
        {
            OnClick = false;
            this.gameObject.layer = 7;
            firstSetting = false;
            switch (direct)
            {
                case 1:
                    ClearRight();
                    break;
                case 2:
                    ClearLeft();
                    break;
                case 3:
                    ClearUp();
                    break;
                case 4:
                    ClearDown();
                    break;
                default:
                    break;
            }

        }
    }
    private void Right()
    {

        for (int i = 0; i < 4; i++)
        {

            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.blue;

                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }

    }
    private void ClearRight()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + j, TileY + i].tag == "Road" || Map.Tile[TileX + j, TileY + i].tag == "Wall")
                    {
                        Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    if (Map.Tile[TileX, TileY + i].tag == "Road" || Map.Tile[TileX, TileY + i].tag == "Wall")
                    {
                        Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
            }
        }
    }

    private void Left()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.blue;

                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }

    }
    private void ClearLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + j, TileY - i].tag == "Road" || Map.Tile[TileX + j, TileY - i].tag == "Wall")
                    {
                        Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    if (Map.Tile[TileX, TileY - i].tag == "Road" || Map.Tile[TileX, TileY - i].tag == "Wall")
                    {
                        Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }
    private void Up()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.blue;

                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }

    }
    private void ClearUp()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX - i, TileY + j].tag == "Road" || Map.Tile[TileX - i, TileY + j].tag == "Wall")
                    {
                        Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }


                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    if (Map.Tile[TileX - i, TileY].tag == "Road" || Map.Tile[TileX - i, TileY].tag == "Wall")
                    {
                        Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }
    private void Down()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.blue;

                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }

        }


    }
    private void ClearDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + i, TileY + j].tag == "Road" || Map.Tile[TileX + i, TileY + j].tag == "Wall")
                    {
                        Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    if (Map.Tile[TileX + i, TileY].tag == "Road" || Map.Tile[TileX + i, TileY].tag == "Wall")
                    {
                        Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }
}
