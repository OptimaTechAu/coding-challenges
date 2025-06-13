using System;
using Xunit;
using Robot_Wars.Implementation;
using System.Collections.Generic;
using static Robot_Wars.Common.Enumerations.Enumerations;
using Robot_Wars.Interfaces;
using Robot_Wars.Common;

namespace Robot_Wars_Test
{
    public class RobotWarsTest
    {
        private Dictionary<CommandInstructions, ICommand> _commands;
        public RobotWarsTest()
        {
            _commands = new Dictionary<CommandInstructions, ICommand>
                    {
                        {CommandInstructions.L,new RotateLeftCommand(new RotateLogic())
                        },
                        { CommandInstructions.R,new RotateRightCommand(new RotateLogic()) },
                        { CommandInstructions.M,new MoveCommand( new MoveLogic()) }
            };
        }
        [Fact]
        public void RobotArena_Size_Should_Be_ThrowsArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => new RobotArena(0, 0));

        }
        [Fact]
        public void RobotArena_Size_Should_Be_GreaterThanZero()
        {
            //arrange
            var arena = new RobotArena(5, 5);

            // act & assert
            Assert.Equal(5, arena.Length);
            Assert.Equal(5, arena.Width);

        }
        [Fact]
        public void Robort_Create_And_Position()
        {
            //arrange
            var arena = new RobotArena(5, 5);
            var robot = new Robot(arena, _commands);
            //act
            robot.Position = new ArenaPoints(1, 2);
            robot.RobotDirection = Direction.N;

            Assert.Equal(1, robot.Position.X);
            Assert.Equal(2, robot.Position.Y);
            Assert.Equal(Direction.N, robot.RobotDirection);

        }

        [Fact]
        public void Robort_Create_And_Position_Send_Commands()
        {
            //arrange
            var arena = new RobotArena(5, 5);
            var robot = new Robot(arena, _commands);
            //act
            robot.Position = new ArenaPoints(1, 2);
            robot.RobotDirection = Direction.N;

            string commands = "LMLMLMLMM";
            foreach (char item in commands)
            {
                CommandInstructions ins;
                if (Enum.TryParse<CommandInstructions>(item.ToString(), out ins))
                {
                    robot.CommandInstruction(ins);
                }

            }
            //1 3 N
            Assert.Equal(1, robot.Position.X);
            Assert.Equal(3, robot.Position.Y);
            Assert.Equal(Direction.N, robot.RobotDirection);

        }
        [Fact]
        public void Robort_Create_And_Position_Send_Commands_Reposition_And_Send_Commands()
        {
            //arrange
            var arena = new RobotArena(5, 5);
            var robot = new Robot(arena, _commands);
            robot.Position = new ArenaPoints(1, 2);
            robot.RobotDirection = Direction.N;

            string commands = "LMLMLMLMM";
            foreach (char item in commands)
            {
                CommandInstructions ins;
                if (Enum.TryParse<CommandInstructions>(item.ToString(), out ins))
                {
                    robot.CommandInstruction(ins);
                }

            }
            //3 3 E
            //Act
            robot.Position = new ArenaPoints(3, 3);
            robot.RobotDirection = Direction.E;

            commands = "MMRMMRMRRM";
            foreach (char item in commands)
            {
                CommandInstructions ins;
                if (Enum.TryParse<CommandInstructions>(item.ToString(), out ins))
                {
                    robot.CommandInstruction(ins);
                }

            }
            //5 1 E
            Assert.Equal(5, robot.Position.X);
            Assert.Equal(1, robot.Position.Y);
            Assert.Equal(Direction.E, robot.RobotDirection);

        }
        [Fact]
        public void Robort_Create_And_Position_And_Rotate_Left_360()
        {
            //arrange
            var arena = new RobotArena(5, 5);
            var robot = new Robot(arena, _commands);
            robot.Position = new ArenaPoints(1, 2);
            robot.RobotDirection = Direction.N;

            string commands = "LLLL";
            foreach (char item in commands)
            {
                CommandInstructions ins;
                if (Enum.TryParse<CommandInstructions>(item.ToString(), out ins))
                {
                    robot.CommandInstruction(ins);
                }

            }
           
            Assert.Equal(1, robot.Position.X);
            Assert.Equal(2, robot.Position.Y);
            Assert.Equal(Direction.N, robot.RobotDirection);

        }
    }
}


